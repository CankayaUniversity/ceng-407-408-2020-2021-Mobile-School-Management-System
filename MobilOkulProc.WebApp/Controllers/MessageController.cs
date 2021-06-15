using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebApp.ViewModels;
using X.PagedList;
using MobilOkulProc.WebApp.Controllers;
using static MobilOkulProc.WebApp.Controllers.HomeController;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MobilOkulProc.WebApp.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<MESSAGE> mb)
        {
            MessageViewModel<MESSAGE> m = new MessageViewModel<MESSAGE>();
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
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.MessageTitle.ToLower().Contains(Search)).ToList();
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
            MessageViewModel<MESSAGE> viewModel = new MessageViewModel<MESSAGE>()
            {
                SenderList = new SelectList(Sender.Liste, "ObjectID", "FullName"),
                ReceiverList = new SelectList(Receiver.Liste, "ObjectID", "FullName"),
                SenderId = -1,
                ReceiverId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MessageViewModel<MESSAGE> m)
        {
            m.Mesajlar.Nesne.SenderID = m.SenderId;
            m.Mesajlar.Nesne.ReceiveID = m.ReceiverId;
            m.Mesajlar = function.Add_Update<MESSAGE>(m.Mesajlar, "Messages/Message_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Message", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            MessageViewModel<MESSAGE> m = new MessageViewModel<MESSAGE>();
            m.Mesajlar = function.Get<MESSAGE>(m.Mesajlar, "Messages/Message_SelectRelational?MessageID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(MessageViewModel<MESSAGE> mb)
        {
            mb.Mesajlar = function.Get<MESSAGE>(mb.Mesajlar, "Messages/Message_Delete?MessageID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Message", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<MESSAGE> m = new Mesajlar<MESSAGE>();
            m = function.Get<MESSAGE>(m, "Messages/Message_Select?MessageID=" + id);
            MessageViewModel<MESSAGE> MessageViewModel = new MessageViewModel<MESSAGE>();

            ViewBag.NameSurname = needs.NameSurname;
            MessageViewModel.Mesajlar = m;
            Mesajlar<USER> Sender = new Mesajlar<USER>();
            Mesajlar<USER> Receiver = new Mesajlar<USER>();
            Sender = function.Get<USER>(Sender, "User/User_Select?UserID=" + m.Nesne.SenderID);
            Receiver = function.Get<USER>(Receiver, "User/User_Select?UserID=" + m.Nesne.ReceiveID);
            MessageViewModel.Mesajlar.Nesne.Sender = Sender.Nesne;
            MessageViewModel.Mesajlar.Nesne.Receive = Receiver.Nesne;
            return View(MessageViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> Sender = new Mesajlar<USER>();
            Mesajlar<USER> Receiver = new Mesajlar<USER>();
            Sender = function.Get<USER>(Sender, "User/User_List");
            Receiver = function.Get<USER>(Receiver, "User/User_List");

            Mesajlar<MESSAGE> Message = new Mesajlar<MESSAGE>();
            Message = function.Get<MESSAGE>(Message, "Messages/Message_SelectRelational?MessageID=" + id);
            MessageViewModel<MESSAGE> MessageViewModel = new MessageViewModel<MESSAGE>()
            {
                SenderList = new SelectList(Sender.Liste, "ObjectID", "FullName"),
                ReceiverList = new SelectList(Receiver.Liste, "ObjectID", "FullName"),
                SenderId = Message.Nesne.SenderID,
                ReceiverId = Message.Nesne.ReceiveID,
            };

            MessageViewModel.Mesajlar = Message;


            ViewBag.NameSurname = needs.NameSurname;
            return View(MessageViewModel);
        }
        [HttpPost]
        public IActionResult Edit(MessageViewModel<MESSAGE> m)
        {
            m.Mesajlar.Nesne.SenderID = m.SenderId;
            m.Mesajlar.Nesne.ReceiveID = m.ReceiverId;
            m.Mesajlar = function.Add_Update<MESSAGE>(m.Mesajlar, "Messages/Message_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Message", m);
        }
    }
}
