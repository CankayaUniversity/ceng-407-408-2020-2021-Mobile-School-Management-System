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
    public class ClassController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<CLASS> mb)
        {
            ClassViewModel<CLASS> m = new ClassViewModel<CLASS>();
            Mesajlar<SCHOOL> User = new Mesajlar<SCHOOL>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<CLASS>(mb, "Class/Class_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<SCHOOL>(User, "School/School_Select?SchoolID=" + item.SchoolID);
                item.School = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.Class_Name.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<SCHOOL> m = new Mesajlar<SCHOOL>();
            m = function.Get<SCHOOL>(m, "School/School_List");
            ClassViewModel<CLASS> viewModel = new ClassViewModel<CLASS>()
            {
                List = new SelectList(m.Liste, "ObjectID", "SchoolName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ClassViewModel<CLASS> m)
        {
            m.Mesajlar.Nesne.SchoolID = m.SelectedId;
            m.Mesajlar = function.Add_Update<CLASS>(m.Mesajlar, "Class/Class_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "School", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            ClassViewModel<CLASS> m = new ClassViewModel<CLASS>();
            m.Mesajlar = function.Get<CLASS>(m.Mesajlar, "Class/Class_SelectRelational?ClassID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ClassViewModel<CLASS> mb)
        {
            mb.Mesajlar = function.Get<CLASS>(mb.Mesajlar, "Class/Class_Delete?ClassID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "School", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<CLASS> m = new Mesajlar<CLASS>();
            m = function.Get<CLASS>(m, "Class/Class_Select?ClassID=" + id);
            ClassViewModel<CLASS> schoolViewModel = new ClassViewModel<CLASS>();

            ViewBag.NameSurname = needs.NameSurname;
            schoolViewModel.Mesajlar = m;
            Mesajlar<SCHOOL> mesajlar = new Mesajlar<SCHOOL>();
            mesajlar = function.Get<SCHOOL>(mesajlar, "School/School_Select?SchoolID=" + m.Nesne.SchoolID);
            schoolViewModel.Mesajlar.Nesne.School = mesajlar.Nesne;
            return View(schoolViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<SCHOOL> m = new Mesajlar<SCHOOL>();
            m = function.Get<SCHOOL>(m, "School/School_List");
            Mesajlar<CLASS> mesajlar = new Mesajlar<CLASS>();
            mesajlar = function.Get<CLASS>(mesajlar, "Class/Class_SelectRelational?ClassID=" + id);
            ClassViewModel<CLASS> schoolViewModel = new ClassViewModel<CLASS>()
            {
                List = new SelectList(m.Liste, "ObjectID", "SchoolName"),
                SelectedId = mesajlar.Nesne.SchoolID
            };

            schoolViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(schoolViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ClassViewModel<CLASS> m)
        {
            m.Mesajlar.Nesne.SchoolID = m.SelectedId;
            m.Mesajlar = function.Add_Update<CLASS>(m.Mesajlar, "Class/Class_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Class", m);
        }
    }

}
