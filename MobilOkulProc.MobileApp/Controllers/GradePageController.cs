using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using static MobilOkulProc.MobileApp.Controllers.HomePageController;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class GradePageController : Controller
    {
        public IActionResult GradePage(int? page, Mesajlar<GRADE> mb)
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


            StudentPageModel<STUDENT> st = new StudentPageModel<STUDENT>();
            Mesajlar<STUDENT> stu = new Mesajlar<STUDENT>();
            st.Mesajlar = function.Get<STUDENT>(stu, "Student/Student_List");

            foreach (var item in st.Mesajlar.Liste)
            {
                if (item.UserID == ViewBag.ObjectID)
                {
                    ViewBag.Student = item.ObjectID;

                }
            }

            if (ViewBag.Usertype == 3)
            {
                ParentPageModel<PARENT> p = new ParentPageModel<PARENT>();
                Mesajlar<PARENT> par = new Mesajlar<PARENT>();
                p.Mesajlar = function.Get<PARENT>(par, "Parent/Parent_List");

                foreach (var item in p.Mesajlar.Liste)
                {
                    foreach (var item2 in st.Mesajlar.Liste)
                    {
                        if (item.UserID == item2.ObjectID)
                        {
                            ViewBag.Student = item2.ObjectID;

                        }
                    }
                }
            }


            GradePageModel<GRADE> m = new GradePageModel<GRADE>();
            Mesajlar<LECTURE> User = new Mesajlar<LECTURE>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<GRADE>(mb, "Grade/Grade_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<LECTURE>(User, "Lecture/Lecture_Select?LectureID=" + item.LectureID);
                item.Lecture = User.Nesne;
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
    }
}