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
           ViewBag.Usertype = HttpContext.Session.GetString("userid");
            //
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

            if (ViewBag.Usertype == "\"Parent\"")
            {
                ParentPageModel<PARENT> p = new ParentPageModel<PARENT>();
                Mesajlar<PARENT> par = new Mesajlar<PARENT>();
                p.Mesajlar = function.Get<PARENT>(par, "Parent/Parent_List");

                
                foreach (var item in p.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.ObjectID)
                    {
                        ViewBag.ParentObjectID = item.ObjectID;
                    }
                    
                }

                StudentParentModel<STUDENT_PARENT> sp = new StudentParentModel<STUDENT_PARENT>();
                Mesajlar<STUDENT_PARENT> spr = new Mesajlar<STUDENT_PARENT>();
                sp.Mesajlar = function.Get<STUDENT_PARENT>(spr, "StudentParent/StudentParent_List");

                foreach (var item in sp.Mesajlar.Liste)
                {
                    if (item.ParentID==ViewBag.ParentObjectID)
                    {
                        ViewBag.Student = item.StudentID;
                    }
                }
            }

            if (ViewBag.Usertype == "\"Teacher\"")
            {
                TeacherPageModel<TEACHER> t = new TeacherPageModel<TEACHER>();
                Mesajlar<TEACHER> te = new Mesajlar<TEACHER>();
                t.Mesajlar = function.Get<TEACHER>(te, "Teacher/Teacher_List");

                foreach (var item in t.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.ObjectID)
                    {
                        ViewBag.TeacherObjectID = item.ObjectID;
                        
                    }
                }

                LecturePageModel<LECTURE> l = new LecturePageModel<LECTURE>();
                Mesajlar<LECTURE> lec = new Mesajlar<LECTURE>();
                l.Mesajlar = function.Get<LECTURE>(lec, "Lecture/Lecture_List");


                foreach (var item in l.Mesajlar.Liste)
                {
                    if (item.TeacherID==ViewBag.TeacherObjectID)
                    {
                        ViewBag.Student = item.TeacherID;
                       
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