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
            Mesajlar<CITY> City = new Mesajlar<CITY>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<EDUCATIONAL_INSTITUTION>(mb, "EducationalInstitution/EducationalInstitution_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                City = function.Get<CITY>(City, "City/City_Select?CITYID=" + item.CityID);
                item.City = City.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.EducationalName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            if (mb.Mesaj != "")
            {
                m.Mesajlar = mb;
            }
            return View(m);
        }
        public IActionResult Add()
        {
            
            Mesajlar<CITY> m = new Mesajlar<CITY>();
            m = function.Get<CITY>(m, "City/City_List");
            EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> viewModel = new EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION>()
            {
                CityList = new SelectList(m.Liste, "ObjectID","CityName"),
                CityID = -1,
            };
            
            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EducationalInstitutionViewModel<EDUCATIONAL_INSTITUTION> m)
        {
            m.Mesajlar.Nesne.CityID = m.CityID;
            m.Mesajlar = function.Add_Update<EDUCATIONAL_INSTITUTION>(m.Mesajlar, "EducationalInstitution/EducationalInstitution_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List","EducationalInstitution",m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            m = function.Get<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<EDUCATIONAL_INSTITUTION> mb)
        {
            mb = function.Get<EDUCATIONAL_INSTITUTION>(mb, "EducationalInstitution/EducationalInstitution_Delete?EducationalInstitutionID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "EducationalInstitution", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            m = function.Get<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            m = function.Get<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<EDUCATIONAL_INSTITUTION> m)
        {
            m = function.Add_Update<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
    }
}
