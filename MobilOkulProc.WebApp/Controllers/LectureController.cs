using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using static MobilOkulProc.WebApp.Controllers.HomeController;


namespace MobilOkulProc.WebApp.Controllers
{
    public class LectureController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<LECTURE> mb)
        {
            LectureViewModel<LECTURE> m = new LectureViewModel<LECTURE>();
            Mesajlar<CLASS_SECTION> Class_Section = new Mesajlar<CLASS_SECTION>();
            Mesajlar<TEACHER> Teacher = new Mesajlar<TEACHER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<LECTURE>(mb, "Lecture/Lecture_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                Class_Section = function.Get<CLASS_SECTION>(Class_Section, "ClassSection/Class_SectionSelect?ClassSectionID=" + item.ClassSectionsID);
                item.ClassSections = Class_Section.Nesne;
                Teacher = function.Get<TEACHER>(Teacher, "Teacher/Teacher_Select?TeacherID=" + item.TeacherID);
                item.Teacher = Teacher.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.LectureName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<CLASS_SECTION> m = new Mesajlar<CLASS_SECTION>();
            m = function.Get<CLASS_SECTION>(m, "ClassSection/ClassSection_List");
            Mesajlar<TEACHER> b = new Mesajlar<TEACHER>();
            b = function.Get<TEACHER>(b, "Teacher/Teacher_List");
            LectureViewModel<LECTURE> viewModel = new LectureViewModel<LECTURE>()
            {
                StudentList = new SelectList(m.Liste, "ObjectID", "ClassSectionName"),
                TeacherList = new SelectList(b.Liste, "ObjectID", "FullName"),
                TeacherId = -1,
                StudentId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(LectureViewModel<LECTURE> m)
        {
            m.Mesajlar.Nesne.ClassSectionsID = m.StudentId;
            m.Mesajlar.Nesne.TeacherID = m.TeacherId;
            m.Mesajlar = function.Add_Update<LECTURE>(m.Mesajlar, "Lecture/Lecture_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Lecture", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            LectureViewModel<LECTURE> m = new LectureViewModel<LECTURE>();
            m.Mesajlar = function.Get<LECTURE>(m.Mesajlar, "Lecture/Lecture_SelectRelational?LectureID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(LectureViewModel<LECTURE> mb)
        {
            mb.Mesajlar = function.Get<LECTURE>(mb.Mesajlar, "Lecture/Lecture_Delete?LectureID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Lecture", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<LECTURE> m = new Mesajlar<LECTURE>();
            m = function.Get<LECTURE>(m, "Lecture/Lecture_Select?LectureID=" + id);
            LectureViewModel<LECTURE> TeacherViewModel = new LectureViewModel<LECTURE>();

            ViewBag.NameSurname = needs.NameSurname;
            TeacherViewModel.Mesajlar = m;
            Mesajlar<CLASS_SECTION> mesajlar = new Mesajlar<CLASS_SECTION>();
            Mesajlar<TEACHER> Teacher = new Mesajlar<TEACHER>();
            mesajlar = function.Get<CLASS_SECTION>(mesajlar, "ClassSection/ClassSection_Select?ClassSectionID=" + m.Nesne.ClassSectionsID);
            Teacher = function.Get<TEACHER>(Teacher, "Teacher/Teacher_Select?TeacherID=" + m.Nesne.TeacherID);
            TeacherViewModel.Mesajlar.Nesne.ClassSections = mesajlar.Nesne;
            TeacherViewModel.Mesajlar.Nesne.Teacher = Teacher.Nesne;
            return View(TeacherViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<CLASS_SECTION> m = new Mesajlar<CLASS_SECTION>();
            Mesajlar<TEACHER> b = new Mesajlar<TEACHER>();
            m = function.Get<CLASS_SECTION>(m, "ClassSection/ClassSection_List");
            b = function.Get<TEACHER>(b, "Teacher/Teacher_List");

            Mesajlar<LECTURE> Lecture = new Mesajlar<LECTURE>();
            Lecture = function.Get<LECTURE>(Lecture, "Lecture/Lecture_SelectRelational?LectureID=" + id);
            LectureViewModel<LECTURE> TeacherViewModel = new LectureViewModel<LECTURE>()
            {
                StudentList = new SelectList(m.Liste, "ObjectID", "ClassSectionName"),
                TeacherList = new SelectList(b.Liste, "ObjectID", "FullName"),
                TeacherId = Lecture.Nesne.TeacherID,
                StudentId = Lecture.Nesne.ClassSectionsID,
            };

            TeacherViewModel.Mesajlar = Lecture;


            ViewBag.NameSurname = needs.NameSurname;
            return View(TeacherViewModel);
        }
        [HttpPost]
        public IActionResult Edit(LectureViewModel<LECTURE> m)
        {
            m.Mesajlar.Nesne.ClassSectionsID = m.StudentId;
            m.Mesajlar.Nesne.TeacherID = m.TeacherId;
            m.Mesajlar = function.Add_Update<LECTURE>(m.Mesajlar, "Lecture/Lecture_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Lecture", m);
        }

        [HttpGet]
        public IActionResult AddToClass()
        {


            Mesajlar<CLASS> c = new Mesajlar<CLASS>();
            Mesajlar<LECTURE> l = new Mesajlar<LECTURE>();
            l = function.Get<LECTURE>(l, "Lecture/Lecture_List");
            c = function.Get<CLASS>(c, "Class/Class_List");
            LectureViewModel<LECTURE> viewModel = new LectureViewModel<LECTURE>()
            {
                StudentList = new SelectList(c.Liste, "ObjectID", "ClassSectionName"),
                StudentId = -1,
                TeacherList = new SelectList(l.Liste, "ObjectID", "LectureName"),
                TeacherId = -1
            };

            return View();
        }
    }
}
