using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using System;
using System.Linq;
using X.PagedList;
using static MobilOkulProc.MobileApp.Controllers.HomePageController;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class FeedbackPageController : Controller
    {
        public IActionResult FeedbackPage()
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Usertype = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            Mesajlar<MESSAGE> notification = new Mesajlar<MESSAGE>();
            MessagePageModel<MESSAGE> notif = new MessagePageModel<MESSAGE>();
            notif.Mesajlar = function.Get<MESSAGE>(notification, "Messages/Message_List");


            int count = 0;
            foreach (var item in notif.Mesajlar.Liste)
            {
                if (item.SenderID == ViewBag.ObjectID || item.ReceiveID == ViewBag.ObjectID)
                {
                    count++;
                }
            }

            ViewBag.Notification = count;

            return View();
        }


        public IActionResult Add()
        {

            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Usertype = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            Mesajlar<MESSAGE> notification = new Mesajlar<MESSAGE>();
            MessagePageModel<MESSAGE> notif = new MessagePageModel<MESSAGE>();
            notif.Mesajlar = function.Get<MESSAGE>(notification, "Messages/Message_List");


            int count = 0;
            foreach (var item in notif.Mesajlar.Liste)
            {
                if (item.SenderID == ViewBag.ObjectID || item.ReceiveID == ViewBag.ObjectID)
                {
                    count++;
                }
            }

            ViewBag.Notification = count;

            if (ViewBag.Usertype == 1)
            {
                Mesajlar<TEACHER> m = new Mesajlar<TEACHER>();
                m = function.Get<TEACHER>(m, "Teacher/Teacher_List");
                FeedbackPageModel<FEEDBACK> viewModel = new FeedbackPageModel<FEEDBACK>()
                {
                    List = new SelectList(m.Liste, "ObjectID", "Name"),
                    SelectedId = -1,
                };


                return View(viewModel);
            }
            else if (ViewBag.Usertype == 2)
            {
                Mesajlar<STUDENT> m = new Mesajlar<STUDENT>();
                m = function.Get<STUDENT>(m, "Student/Student_List");
                FeedbackPageModel<FEEDBACK> viewModel = new FeedbackPageModel<FEEDBACK>()
                {
                    List = new SelectList(m.Liste, "ObjectID", "StdName"),
                    SelectedId = -1,
                };


                return View(viewModel);
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FeedbackPageModel<FEEDBACK> m)
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Usertype = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");




            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar.Nesne.FeedbackDate = DateTime.Now;
            m.Mesajlar.Nesne.FeedbackType = 1;
            m.Mesajlar.Nesne.Status = true;

            m.Mesajlar = function.Add_Update<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "FeedbackPage", m.Mesajlar);
        }
        public IActionResult List(string Search, int? page, Mesajlar<FEEDBACK> mb)
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Usertype = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            Mesajlar<MESSAGE> notification = new Mesajlar<MESSAGE>();
            MessagePageModel<MESSAGE> notif = new MessagePageModel<MESSAGE>();
            notif.Mesajlar = function.Get<MESSAGE>(notification, "Messages/Message_List");


            if (ViewBag.Usertype == 2)
            {
                TeacherPageModel<TEACHER> t = new TeacherPageModel<TEACHER>();
                Mesajlar<TEACHER> te = new Mesajlar<TEACHER>();
                t.Mesajlar = function.Get<TEACHER>(te, "Teacher/Teacher_List");

                foreach (var item in t.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.ObjectID)
                    {
                        ViewBag.TeacherID = item.ObjectID;
                    }
                }
            }



            int count = 0;
            foreach (var item in notif.Mesajlar.Liste)
            {
                if (item.SenderID == ViewBag.ObjectID || item.ReceiveID == ViewBag.ObjectID)
                {
                    count++;
                }
            }

            ViewBag.Notification = count;

            FeedbackPageModel<FEEDBACK> m = new FeedbackPageModel<FEEDBACK>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<FEEDBACK>(mb, "Feedback/Feedback_List");

            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));



            foreach (var item in m.Mesajlar.Liste)
            {

                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;

            }

            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);

        }


    }

}