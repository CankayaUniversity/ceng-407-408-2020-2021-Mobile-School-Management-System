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
        public async Task<IActionResult> Mailbox()
        {
            Mesajlar<MESSAGE> messageList = new Mesajlar<MESSAGE>();
            messageList = await functions.Get<MESSAGE>(messageList, "Messages/Message_ListRelationalReceiver?ReceiveID=" + needs.UserID);
            _cache.Set("MessageCount", messageList.Liste.Count.ToString());
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
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
        [HttpPost]
        public async Task<IActionResult> ComposeBy(int Id)
        {
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View();

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
