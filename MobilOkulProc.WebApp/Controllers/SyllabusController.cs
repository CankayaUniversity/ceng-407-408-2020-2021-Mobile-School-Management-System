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
    public class SyllabusController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<SYLLABUS> mb)
        {
            SyllabusViewModel<SYLLABUS> m = new SyllabusViewModel<SYLLABUS>();
            Mesajlar<DAYS> Days = new Mesajlar<DAYS>();
            Mesajlar<LECTURE> Lecture = new Mesajlar<LECTURE>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<SYLLABUS>(mb, "Syllabus/Syllabus_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                Days = function.Get<DAYS>(Days, "Days/Days_Select?DaysID=" + item.DaysID);
                item.Days = Days.Nesne;
                Lecture = function.Get<LECTURE>(Lecture, "Lecture/Lecture_Select?LectureID=" + item.LectureID);
                item.Lecture = Lecture.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.Lecture.LectureName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<DAYS> m = new Mesajlar<DAYS>();
            m = function.Get<DAYS>(m, "Days/Days_List");
            Mesajlar<LECTURE> b = new Mesajlar<LECTURE>();
            b = function.Get<LECTURE>(b, "Lecture/Lecture_List");
            SyllabusViewModel<SYLLABUS> viewModel = new SyllabusViewModel<SYLLABUS>()
            {
                DaysList = new SelectList(m.Liste, "ObjectID", "DayName"),
                LectureList = new SelectList(b.Liste, "ObjectID", "LectureName"),
                LectureId = -1,
                DaysId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SyllabusViewModel<SYLLABUS> m)
        {
            m.Mesajlar.Nesne.DaysID = m.DaysId;
            m.Mesajlar.Nesne.LectureID = m.LectureId;
            m.Mesajlar = function.Add_Update<SYLLABUS>(m.Mesajlar, "Syllabus/Syllabus_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Syllabus", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            SyllabusViewModel<SYLLABUS> m = new SyllabusViewModel<SYLLABUS>();
            m.Mesajlar = function.Get<SYLLABUS>(m.Mesajlar, "Syllabus/Syllabus_SelectRelational?SyllabusID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SyllabusViewModel<SYLLABUS> mb)
        {
            mb.Mesajlar = function.Get<SYLLABUS>(mb.Mesajlar, "Syllabus/Syllabus_Delete?SyllabusID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Syllabus", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<SYLLABUS> m = new Mesajlar<SYLLABUS>();
            m = function.Get<SYLLABUS>(m, "Syllabus/Syllabus_Select?SyllabusID=" + id);
            SyllabusViewModel<SYLLABUS> TeacherViewModel = new SyllabusViewModel<SYLLABUS>();

            ViewBag.NameSurname = needs.NameSurname;
            TeacherViewModel.Mesajlar = m;
            Mesajlar<DAYS> mesajlar = new Mesajlar<DAYS>();
            Mesajlar<LECTURE> Lecture = new Mesajlar<LECTURE>();
            mesajlar = function.Get<DAYS>(mesajlar, "Days/Days_Select?DaysID=" + m.Nesne.DaysID);
            Lecture = function.Get<LECTURE>(Lecture, "Lecture/Lecture_Select?LectureID=" + m.Nesne.LectureID);
            TeacherViewModel.Mesajlar.Nesne.Days = mesajlar.Nesne;
            TeacherViewModel.Mesajlar.Nesne.Lecture = Lecture.Nesne;
            return View(TeacherViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<DAYS> m = new Mesajlar<DAYS>();
            Mesajlar<LECTURE> b = new Mesajlar<LECTURE>();
            m = function.Get<DAYS>(m, "Days/Days_List");
            b = function.Get<LECTURE>(b, "Lecture/Lecture_List");

            Mesajlar<SYLLABUS> teacher = new Mesajlar<SYLLABUS>();
            teacher = function.Get<SYLLABUS>(teacher, "Syllabus/Syllabus_SelectRelational?SyllabusID=" + id);
            SyllabusViewModel<SYLLABUS> TeacherViewModel = new SyllabusViewModel<SYLLABUS>()
            {
                DaysList = new SelectList(m.Liste, "ObjectID", "DayName"),
                LectureList = new SelectList(b.Liste, "ObjectID", "LectureName"),
                LectureId = teacher.Nesne.LectureID,
                DaysId = teacher.Nesne.DaysID,
            };

            TeacherViewModel.Mesajlar = teacher;


            ViewBag.NameSurname = needs.NameSurname;
            return View(TeacherViewModel);
        }
        [HttpPost]
        public IActionResult Edit(SyllabusViewModel<SYLLABUS> m)
        {
            m.Mesajlar.Nesne.DaysID = m.DaysId;
            m.Mesajlar.Nesne.LectureID = m.LectureId;
            m.Mesajlar = function.Add_Update<SYLLABUS>(m.Mesajlar, "Syllabus/Syllabus_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Syllabus", m);
        }
    }
}
