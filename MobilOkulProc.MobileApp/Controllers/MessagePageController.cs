using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using System.Linq;
using X.PagedList;

using static MobilOkulProc.MobileApp.Controllers.HomePageController;
using Microsoft.AspNetCore.Http;
using System;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class MessagePageController : Controller
    {

        public IActionResult MessagePage()
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            
            return View();
        }
        public IActionResult List(string Search, int? page, Mesajlar<MESSAGE> mb)
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            MessagePageModel<MESSAGE> m = new MessagePageModel<MESSAGE>();
            Mesajlar<USER> Sender = new Mesajlar<USER>();
            Mesajlar<USER> Receiver = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<MESSAGE>(mb, "Messages/Message_List");

 

            foreach (var item in m.Mesajlar.Liste)
            {

                    Sender = function.Get<USER>(Sender, "User/User_Select?UserID=" + item.SenderID);

                    item.Sender = Sender.Nesne;
                

 
                    Receiver = function.Get<USER>(Sender, "User/User_Select?UserID=" + item.ReceiveID);

                    item.Receive = Receiver.Nesne;
                
            }

            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<USER> Sender = new Mesajlar<USER>();
            Sender = function.Get<USER>(Sender, "User/User_List");
            Mesajlar<USER> Receiver = new Mesajlar<USER>();
            Receiver = function.Get<USER>(Receiver, "User/User_List");
            MessagePageModel<MESSAGE> viewModel = new MessagePageModel<MESSAGE>()
            {
                SenderList = new SelectList(Sender.Liste, "ObjectID", "NameSurname"),
                ReceiverList = new SelectList(Receiver.Liste, "ObjectID", "NameSurname"),
                SenderId = -1,
                ReceiverId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MessagePageModel<MESSAGE> m)
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = HttpContext.Session.GetString("no");
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            var userid= int.Parse(HttpContext.Session.GetString("no"));
            m.Mesajlar.Nesne.SenderID = userid;

            m.Mesajlar.Nesne.PriorityID = 1;
            m.Mesajlar.Nesne.SendTime = DateTime.Now;

            string date = "1111 - 11 - 11 11:00:00.0000000";          
            m.Mesajlar.Nesne.ReadTime = DateTime.Parse(date);

           
            m.Mesajlar.Nesne.MessageType = true;
            m.Mesajlar.Nesne.ReceiveID = m.ReceiverId;
            m.Mesajlar.Nesne.Status = true;
            m.Mesajlar = function.Add_Update<MESSAGE>(m.Mesajlar, "Messages/Message_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "MessagePage", m.Mesajlar);
        }       
       
    }
}
