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
    public class GradeController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<GRADE> mb)
        {
            GradeViewModel<GRADE> m = new GradeViewModel<GRADE>();
            Mesajlar<LECTURE> User = new Mesajlar<LECTURE>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<GRADE>(mb, "Grade/Grade_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<LECTURE>(User, "Lecture/Lecture_Select?LectureID=" + item.LectureID);
                item.Lecture = User.Nesne;
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

            Mesajlar<LECTURE> m = new Mesajlar<LECTURE>();
            m = function.Get<LECTURE>(m, "Lecture/Lecture_List");
            GradeViewModel<GRADE> viewModel = new GradeViewModel<GRADE>()
            {
                List = new SelectList(m.Liste, "ObjectID", "LectureName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(GradeViewModel<GRADE> m)
        {
            m.Mesajlar.Nesne.LectureID = m.SelectedId;
            m.Mesajlar = function.Add_Update<GRADE>(m.Mesajlar, "Grade/Grade_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Grade", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            GradeViewModel<GRADE> m = new GradeViewModel<GRADE>();
            m.Mesajlar = function.Get<GRADE>(m.Mesajlar, "Grade/Grade_SelectRelational?GradeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(GradeViewModel<GRADE> mb)
        {
            mb.Mesajlar = function.Get<GRADE>(mb.Mesajlar, "Grade/Grade_Delete?GradeID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Grade", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<GRADE> m = new Mesajlar<GRADE>();
            m = function.Get<GRADE>(m, "Grade/Grade_Select?GradeID=" + id);
            GradeViewModel<GRADE> GradeViewModel = new GradeViewModel<GRADE>();

            ViewBag.NameSurname = needs.NameSurname;
            GradeViewModel.Mesajlar = m;
            Mesajlar<LECTURE> mesajlar = new Mesajlar<LECTURE>();
            mesajlar = function.Get<LECTURE>(mesajlar, "Lecture/Lecture_Select?LectureID=" + m.Nesne.LectureID);
            GradeViewModel.Mesajlar.Nesne.Lecture = mesajlar.Nesne;
            return View(GradeViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<LECTURE> m = new Mesajlar<LECTURE>();
            m = function.Get<LECTURE>(m, "Lecture/Lecture_List");
            Mesajlar<GRADE> mesajlar = new Mesajlar<GRADE>();
            mesajlar = function.Get<GRADE>(mesajlar, "Grade/Grade_SelectRelational?GradeID=" + id);
            GradeViewModel<GRADE> GradeViewModel = new GradeViewModel<GRADE>()
            {
                List = new SelectList(m.Liste, "ObjectID", "LectureName"),
                SelectedId = mesajlar.Nesne.LectureID
            };

            GradeViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(GradeViewModel);
        }
        [HttpPost]
        public IActionResult Edit(GradeViewModel<GRADE> m)
        {
            m.Mesajlar.Nesne.LectureID = m.SelectedId;
            m.Mesajlar = function.Add_Update<GRADE>(m.Mesajlar, "Grade/Grade_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Grade", m);
        }
    }
}
