using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebUserApp.Controllers.HomeController;

namespace MobilOkulProc.WebUserApp.Controllers
{
    public class MessageController : Controller
    {
        private IMemoryCache _cache;
        public MessageController(IConfiguration cfg, IMemoryCache memoryCache)
        {

            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
            _cache = memoryCache;

        }
        public async Task<IActionResult> Sentbox()
        {
            List<MESSAGE> deletedBox = _cache.Get("Deletedbox") as List<MESSAGE>;
            List<MESSAGE> Sentbox = _cache.Get("Sentbox") as List<MESSAGE>;
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.DeletedMessagesCount = deletedBox.Count.ToString();
            ViewBag.SentMessagesCount = Sentbox.Count.ToString();
            ViewBag.MessageCount = _cache.Get("MessageCount") as string;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(Sentbox);
        }
        public async Task<IActionResult> Deletedbox()
        {
            List<MESSAGE> deletedBox = _cache.Get("Deletedbox") as List<MESSAGE>;
            List<MESSAGE> Sentbox = _cache.Get("Sentbox") as List<MESSAGE>;
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.DeletedMessagesCount = deletedBox.Count.ToString();
            ViewBag.SentMessagesCount = Sentbox.Count.ToString();
            ViewBag.MessageCount = _cache.Get("MessageCount") as string;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(deletedBox);
        }
        public async Task<IActionResult> Mailbox()
        {
            #region Get all the messages that are not deleted to use it in mailbox
            Mesajlar<MESSAGE> messageList = new Mesajlar<MESSAGE>();
            messageList = await functions.Get<MESSAGE>(messageList, "Messages/Message_ListRelationalReceiver?ReceiveID=" + needs.UserID);
            _cache.Set("MessageCount", messageList.Liste.Count.ToString());
            _cache.Set("Messages", messageList.Liste);
            #endregion

            #region Get send messages and put it into cache to use it in deletedbox
            List<MESSAGE> Sentbox = new List<MESSAGE>();
            foreach (var item in messageList.Liste)
            {
                if (item.SenderID == needs.UserID)
                {
                    Sentbox.Add(item);
                }
            }
            _cache.Set("Sentbox", Sentbox);
            #endregion

            #region Get deleted(Status == false) messages and put it into cache to use it in sentbox
            Mesajlar<MESSAGE> deletedList = new Mesajlar<MESSAGE>();
            deletedList = await functions.Get<MESSAGE>(deletedList, "Messages/Message_ListRelationalReceiverDeleted?ReceiveID=" + needs.UserID);
            _cache.Set("Deletedbox", deletedList.Liste); 
            #endregion
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.DeletedMessagesCount = deletedList.Liste.Count.ToString();
            ViewBag.SentMessagesCount = Sentbox.Count.ToString();
            ViewBag.MessageCount = messageList.Liste.Count.ToString();
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion

            return View(messageList.Liste);
        }
        public async Task<IActionResult> Compose()
        {
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.MessageCount = _cache.Get("MessageCount") as string;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Compose(Mesajlar<MESSAGE> msg)
        {
            Mesajlar<USER> usr = new Mesajlar<USER>();
            usr = await functions.Get<USER>(usr, "User/User_SelectUsername?Username=" + msg.Nesne.Receive.Username);
            if (usr.Nesne != null)
            {
                if (!string.IsNullOrEmpty(usr.Nesne.Username))
                {
                    msg.Nesne.Receive = usr.Nesne;
                }
                else
                {
                    ViewBag.Result = "Böyle bir kullanıcı bulunmamaktadır";
                    ViewBag.Status = "danger";
                    return View();
                }
            }
           
            msg.Nesne.Status = true;
            msg.Nesne.PriorityID = 1;
            msg.Nesne.SendTime = DateTime.Now;
            msg.Nesne.MessageType = true;
            usr = await functions.Get<USER>(usr, "User/User_Select?UserID=" + needs.UserID);
            msg.Nesne.Sender = usr.Nesne;
            
            msg = await functions.Add_Update<MESSAGE>(msg, "Messages/Message_Insert");





            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.MessageCount = _cache.Get("MessageCount") as string;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            ViewBag.Result = "Mesaj başarıyla gönderildi.";
            ViewBag.Status = "succes";
            return View(msg);
        }
        public async Task<IActionResult> ComposeBy(int Id)
        {
            List<MESSAGE> deletedBox = _cache.Get("Deletedbox") as List<MESSAGE>;
            List<MESSAGE> Sentbox = _cache.Get("Sentbox") as List<MESSAGE>;
            List<MESSAGE> Messages = _cache.Get("Messages") as List<MESSAGE>;

            Mesajlar<MESSAGE> msg = new Mesajlar<MESSAGE>();
            msg.Nesne = Messages.Where(m => m.ObjectID == Id).SingleOrDefault();
            msg.Nesne.MessageTitle = "Re: " + msg.Nesne.MessageTitle;
            msg.Nesne.MessageContent = "\n" + "Re: \"" + msg.Nesne.MessageContent + "\"";
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.DeletedMessagesCount = deletedBox.Count.ToString();
            ViewBag.SentMessagesCount = Sentbox.Count.ToString();
            ViewBag.MessageCount = _cache.Get("MessageCount") as string;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(msg);

        }
       
        public async Task<IActionResult> Read(int Id)
        {
            Mesajlar<MESSAGE> message = new Mesajlar<MESSAGE>();
            Mesajlar<MESSAGE> updateMessage = new Mesajlar<MESSAGE>();
            message = await functions.Get<MESSAGE>(message, "Messages/Message_SelectRelational?MessageID=" + Id);
            if (message.Nesne.ReceiveID == needs.UserID)
            {
                message.Nesne.ReadTime = DateTime.Now;
                await functions.Add_Update<MESSAGE>(message, "Messages/Message_Update?MessageID=" + Id);
            }
            else
            {
                #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
                ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
                ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
                #endregion

                #region Sidebar FullName and Role
                ViewBag.Role = needs.LoginAs;
                ViewBag.FullName = needs.NameSurname;
                #endregion
                return RedirectToAction("Mailbox", "Message");
            }
           
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.MessageCount = _cache.Get("MessageCount") as string;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(message.Nesne);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            Mesajlar<MESSAGE> mesaj = new Mesajlar<MESSAGE>();

            mesaj = await functions.Get<MESSAGE>(mesaj, "Messages/Message_SelectRelational?MessageID=" + Id);
            if (mesaj.Nesne.ReceiveID == needs.UserID)
            {
                mesaj = await functions.Get<MESSAGE>(mesaj, "Messages/Message_Delete?MessageID=" + Id);
            }

            
            

            return RedirectToAction("Mailbox", "Message");
        }
    }
}
