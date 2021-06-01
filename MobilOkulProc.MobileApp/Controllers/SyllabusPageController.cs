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
    public class SyllabusPageController : Controller
    {
        public IActionResult SyllabusPage(int? page)
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            SyllabusPageModel<SYLLABUS> m = new SyllabusPageModel<SYLLABUS>();
            Mesajlar<DAYS> Days = new Mesajlar<DAYS>();
            Mesajlar<LECTURE> Lecture = new Mesajlar<LECTURE>();
            ViewBag.NameSurname = needs.NameSurname;
            Mesajlar<SYLLABUS> mb = new Mesajlar<SYLLABUS>();
            m.Mesajlar = function.Get<SYLLABUS>(mb, "Syllabus/Syllabus_List");


            StudentPageModel<STUDENT> st = new StudentPageModel<STUDENT>();
            Mesajlar<STUDENT> stu = new Mesajlar<STUDENT>();
            st.Mesajlar = function.Get<STUDENT>(stu, "Student/Student_List");

            foreach (var item in st.Mesajlar.Liste)
            {
                if (item.UserID == ViewBag.Userno)
                {
                    ViewBag.Student = item.ObjectID;

                }
            }

            foreach (var item in m.Mesajlar.Liste)
            {

                Days = function.Get<DAYS>(Days, "Days/Days_Select?DaysID=" + item.DaysID);
                item.Days = Days.Nesne;
                Lecture = function.Get<LECTURE>(Lecture, "Lecture/Lecture_Select?LectureID=" + item.LectureID);
                item.Lecture = Lecture.Nesne;


            }



            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }

    }
}