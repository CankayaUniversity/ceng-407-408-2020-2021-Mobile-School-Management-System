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
    public class AbsenceController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<ABSENCE> mb)
        {
            AbsenceViewModel<ABSENCE> m = new AbsenceViewModel<ABSENCE>();
            Mesajlar<STUDENT> User = new Mesajlar<STUDENT>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<ABSENCE>(mb, "Absence/Absence_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<STUDENT>(User, "Student/Student_Select?StudentID=" + item.StudentID);
                item.Student = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.AbsenceDetails.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<STUDENT> m = new Mesajlar<STUDENT>();
            m = function.Get<STUDENT>(m, "Student/Student_List");
            AbsenceViewModel<ABSENCE> viewModel = new AbsenceViewModel<ABSENCE>()
            {
                List = new SelectList(m.Liste, "ObjectID", "FullName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AbsenceViewModel<ABSENCE> m)
        {
            m.Mesajlar.Nesne.StudentID = m.SelectedId;
            m.Mesajlar = function.Add_Update<ABSENCE>(m.Mesajlar, "Absence/Absence_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Absence", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            AbsenceViewModel<ABSENCE> m = new AbsenceViewModel<ABSENCE>();
            m.Mesajlar = function.Get<ABSENCE>(m.Mesajlar, "Absence/Absence_SelectRelational?AbsenceID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AbsenceViewModel<ABSENCE> mb)
        {
            mb.Mesajlar = function.Get<ABSENCE>(mb.Mesajlar, "Absence/Absence_Delete?AbsenceID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Absence", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<ABSENCE> m = new Mesajlar<ABSENCE>();
            m = function.Get<ABSENCE>(m, "Absence/Absence_Select?AbsenceID=" + id);
            AbsenceViewModel<ABSENCE> ViewModel = new AbsenceViewModel<ABSENCE>();

            ViewBag.NameSurname = needs.NameSurname;
            ViewModel.Mesajlar = m;
            Mesajlar<STUDENT> mesajlar = new Mesajlar<STUDENT>();
            mesajlar = function.Get<STUDENT>(mesajlar, "Student/Student_Select?StudentID=" + m.Nesne.StudentID);
            ViewModel.Mesajlar.Nesne.Student = mesajlar.Nesne;
            return View(ViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<STUDENT> m = new Mesajlar<STUDENT>();
            m = function.Get<STUDENT>(m, "Student/Student_List");
            Mesajlar<ABSENCE> mesajlar = new Mesajlar<ABSENCE>();
            mesajlar = function.Get<ABSENCE>(mesajlar, "Absence/Absence_SelectRelational?AbsenceID=" + id);
            AbsenceViewModel<ABSENCE> ViewModel = new AbsenceViewModel<ABSENCE>()
            {
                List = new SelectList(m.Liste, "ObjectID", "FullName"),
                SelectedId = mesajlar.Nesne.StudentID
            };

            ViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult Edit(AbsenceViewModel<ABSENCE> m)
        {
            m.Mesajlar.Nesne.StudentID = m.SelectedId;
            m.Mesajlar = function.Add_Update<ABSENCE>(m.Mesajlar, "Absence/Absence_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Absence", m);
        }
    }

}
