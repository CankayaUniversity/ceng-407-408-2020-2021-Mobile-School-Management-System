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
    public class SectionController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<SECTION> mb)
        {
            SectionViewModel<SECTION> m = new SectionViewModel<SECTION>();
            Mesajlar<SCHOOL> User = new Mesajlar<SCHOOL>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<SECTION>(mb, "Section/Section_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<SCHOOL>(User, "School/School_Select?SchoolID=" + item.SchoolID);
                item.School = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.SectionName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<SCHOOL> m = new Mesajlar<SCHOOL>();
            m = function.Get<SCHOOL>(m, "School/School_List");
            SectionViewModel<SECTION> viewModel = new SectionViewModel<SECTION>()
            {
                List = new SelectList(m.Liste, "ObjectID", "SchoolName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SectionViewModel<SECTION> m)
        {
            m.Mesajlar.Nesne.SchoolID = m.SelectedId;
            m.Mesajlar = function.Add_Update<SECTION>(m.Mesajlar, "Section/Section_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Section", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            SectionViewModel<SECTION> m = new SectionViewModel<SECTION>();
            m.Mesajlar = function.Get<SECTION>(m.Mesajlar, "Section/Section_SelectRelational?SectionID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SectionViewModel<SECTION> mb)
        {
            mb.Mesajlar = function.Get<SECTION>(mb.Mesajlar, "Section/Section_Delete?SectionID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Section", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<SECTION> m = new Mesajlar<SECTION>();
            m = function.Get<SECTION>(m, "Section/Section_Select?SectionID=" + id);
            SectionViewModel<SECTION> SectionViewModel = new SectionViewModel<SECTION>();

            ViewBag.NameSurname = needs.NameSurname;
            SectionViewModel.Mesajlar = m;
            Mesajlar<SCHOOL> mesajlar = new Mesajlar<SCHOOL>();
            mesajlar = function.Get<SCHOOL>(mesajlar, "School/School_Select?SchoolID=" + m.Nesne.SchoolID);
            SectionViewModel.Mesajlar.Nesne.School = mesajlar.Nesne;
            return View(SectionViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<SCHOOL> m = new Mesajlar<SCHOOL>();
            m = function.Get<SCHOOL>(m, "School/School_List");
            Mesajlar<SECTION> mesajlar = new Mesajlar<SECTION>();
            mesajlar = function.Get<SECTION>(mesajlar, "Section/Section_SelectRelational?SectionID=" + id);
            SectionViewModel<SECTION> SectionViewModel = new SectionViewModel<SECTION>()
            {
                List = new SelectList(m.Liste, "ObjectID", "SchoolName"),
                SelectedId = mesajlar.Nesne.SchoolID
            };

            SectionViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(SectionViewModel);
        }
        [HttpPost]
        public IActionResult Edit(SectionViewModel<SECTION> m)
        {
            m.Mesajlar.Nesne.SchoolID = m.SelectedId;
            m.Mesajlar = function.Add_Update<SECTION>(m.Mesajlar, "Section/Section_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Section", m);
        }
    }
}
