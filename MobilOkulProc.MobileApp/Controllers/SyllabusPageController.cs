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

            if (ViewBag.Userid == 1 || ViewBag.Userid == 3)
            {


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
            else if (ViewBag.Userid == 2)
            {
                TeacherPageModel<TEACHER> t = new TeacherPageModel<TEACHER>();
                Mesajlar<TEACHER> te = new Mesajlar<TEACHER>();
                t.Mesajlar = function.Get<TEACHER>(te, "Teacher/Teacher_List");


                LecturePageModel<LECTURE> l = new LecturePageModel<LECTURE>();
                Mesajlar<LECTURE> le = new Mesajlar<LECTURE>();
                l.Mesajlar = function.Get<LECTURE>(le, "Lecture/Lecture_List");

                foreach (var item in t.Mesajlar.Liste)
                {
                    foreach (var item2 in l.Mesajlar.Liste)
                    {
                        if (item.ObjectID == item2.TeacherID && ViewBag.Userno==item.UserID)
                        {
                            ViewBag.Teacher = item.ObjectID;

                        }
                    }
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