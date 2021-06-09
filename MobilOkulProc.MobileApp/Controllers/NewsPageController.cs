using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using static MobilOkulProc.MobileApp.Controllers.HomePageController;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class NewsPageController : Controller
    {
        public IActionResult NewsPage()
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

        public IActionResult List(string Search, int? page, Mesajlar<NEWS> mb)
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



            NewsPageModel<NEWS> m = new NewsPageModel<NEWS>();
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<NEWS>(mb, "News/News_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                EdIns = function.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + item.EducationID);
                item.Educational_Institution = EdIns.Nesne;
                School = function.Get<SCHOOL>(School, "School/School_Select?SchoolID=" + item.SchoolID);
                item.School = School.Nesne;
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.NewsContent.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
    }
}