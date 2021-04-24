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
    public class EducationalInstitution : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<EDUCATIONAL_INSTITUTION> mb)
        {
            EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> m = new EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION>();
            Mesajlar<CITY> User = new Mesajlar<CITY>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<EDUCATIONAL_INSTITUTION>(mb, "EducationalInstitution/EducationalInstitution_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<CITY>(User, "City/City_Select?CityID=" + item.CityID);
                item.City = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.EducationalName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<CITY> m = new Mesajlar<CITY>();
            m = function.Get<CITY>(m, "City/City_List");
            EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> viewModel = new EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION>()
            {
                List = new SelectList(m.Liste, "ObjectID", "CityName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> m)
        {
            m.Mesajlar.Nesne.CityID = m.SelectedId;
            m.Mesajlar = function.Add_Update<EDUCATIONAL_INSTITUTION>(m.Mesajlar, "EducationalInstitution/EducationalInstitution_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "EducationalInstitution", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> m = new EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION>();
            m.Mesajlar = function.Get<EDUCATIONAL_INSTITUTION>(m.Mesajlar, "EducationalInstitution/EducationalInstitution_SelectRelational?EducationalInstitutionID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> mb)
        {
            mb.Mesajlar = function.Get<EDUCATIONAL_INSTITUTION>(mb.Mesajlar, "EducationalInstitution/EducationalInstitution_Delete?EducationalInstitutionID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "EducationalInstitution", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<EDUCATIONAL_INSTITUTION> m = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            m = function.Get<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + id);
            EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> EducationalInstitutionViewModel = new EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION>();

            ViewBag.NameSurname = needs.NameSurname;
            EducationalInstitutionViewModel.Mesajlar = m;
            Mesajlar<CITY> mesajlar = new Mesajlar<CITY>();
            mesajlar = function.Get<CITY>(mesajlar, "City/City_Select?CityID=" + m.Nesne.CityID);
            EducationalInstitutionViewModel.Mesajlar.Nesne.City = mesajlar.Nesne;
            return View(EducationalInstitutionViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<CITY> m = new Mesajlar<CITY>();
            m = function.Get<CITY>(m, "City/City_List");
            Mesajlar<EDUCATIONAL_INSTITUTION> mesajlar = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            mesajlar = function.Get<EDUCATIONAL_INSTITUTION>(mesajlar, "EducationalInstitution/EducationalInstitution_SelectRelational?EducationalInstitutionID=" + id);
            EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> EducationalInstitutionViewModel = new EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION>()
            {
                List = new SelectList(m.Liste, "ObjectID", "CityName"),
                SelectedId = mesajlar.Nesne.CityID
            };

            EducationalInstitutionViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(EducationalInstitutionViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> m)
        {
            m.Mesajlar.Nesne.CityID = m.SelectedId;
            m.Mesajlar = function.Add_Update<EDUCATIONAL_INSTITUTION>(m.Mesajlar, "EducationalInstitution/EducationalInstitution_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "EducationalInstitution", m);
        }
    }
}
