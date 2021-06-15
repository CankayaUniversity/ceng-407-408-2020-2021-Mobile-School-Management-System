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
    public class ExamController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<EXAM> mb)
        {
            ExamViewModel<EXAM> m = new ExamViewModel<EXAM>();
            Mesajlar<CLASS_SECTION> User = new Mesajlar<CLASS_SECTION>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<EXAM>(mb, "Exam/Exam_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<CLASS_SECTION>(User, "ClassSection/ClassSection_Select?ClassSectionID=" + item.ClassSectionsID);
                item.ClassSections = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.ExamDetails.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<CLASS_SECTION> m = new Mesajlar<CLASS_SECTION>();
            m = function.Get<CLASS_SECTION>(m, "ClassSection/ClassSection_List");
            ExamViewModel<EXAM> viewModel = new ExamViewModel<EXAM>()
            {
                List = new SelectList(m.Liste, "ObjectID", "ClassSectionName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ExamViewModel<EXAM> m)
        {
            m.Mesajlar.Nesne.ClassSectionsID = m.SelectedId;
            m.Mesajlar = function.Add_Update<EXAM>(m.Mesajlar, "Exam/Exam_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Exam", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            ExamViewModel<EXAM> m = new ExamViewModel<EXAM>();
            m.Mesajlar = function.Get<EXAM>(m.Mesajlar, "Exam/Exam_SelectRelational?ExamID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ExamViewModel<EXAM> mb)
        {
            mb.Mesajlar = function.Get<EXAM>(mb.Mesajlar, "Exam/Exam_Delete?ExamID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Exam", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<EXAM> m = new Mesajlar<EXAM>();
            m = function.Get<EXAM>(m, "Exam/Exam_Select?ExamID=" + id);
            ExamViewModel<EXAM> ViewModel = new ExamViewModel<EXAM>();

            ViewBag.NameSurname = needs.NameSurname;
            ViewModel.Mesajlar = m;
            Mesajlar<CLASS_SECTION> mesajlar = new Mesajlar<CLASS_SECTION>();
            mesajlar = function.Get<CLASS_SECTION>(mesajlar, "ClassSection/ClassSection_Select?ClassSectionID=" + m.Nesne.ClassSectionsID);
            ViewModel.Mesajlar.Nesne.ClassSections = mesajlar.Nesne;
            return View(ViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<CLASS_SECTION> m = new Mesajlar<CLASS_SECTION>();
            m = function.Get<CLASS_SECTION>(m, "ClassSection/ClassSection_List");
            Mesajlar<EXAM> mesajlar = new Mesajlar<EXAM>();
            mesajlar = function.Get<EXAM>(mesajlar, "Exam/Exam_SelectRelational?ExamID=" + id);
            ExamViewModel<EXAM> ViewModel = new ExamViewModel<EXAM>()
            {
                List = new SelectList(m.Liste, "ObjectID", "ClassSectionName"),
                SelectedId = mesajlar.Nesne.ClassSectionsID
            };

            ViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ExamViewModel<EXAM> m)
        {
            m.Mesajlar.Nesne.ClassSectionsID = m.SelectedId;
            m.Mesajlar = function.Add_Update<EXAM>(m.Mesajlar, "Exam/Exam_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Exam", m);
        }
    }

}
