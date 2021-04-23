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
    public class SchoolController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<SCHOOL> mb)
        {
            SchoolViewModel<SCHOOL> m = new SchoolViewModel<SCHOOL>();
            Mesajlar<EDUCATIONAL_INSTITUTION> User = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<SCHOOL>(mb, "School/School_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<EDUCATIONAL_INSTITUTION>(User, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + item.EducationID);
                item.Education = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.SchoolName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<EDUCATIONAL_INSTITUTION> m = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            m = function.Get<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_List");
            SchoolViewModel<SCHOOL> viewModel = new SchoolViewModel<SCHOOL>()
            {
                List = new SelectList(m.Liste, "ObjectID", "EducationalName"),
                Id = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SchoolViewModel<SCHOOL> m)
        {
            m.Mesajlar.Nesne.EducationID = m.Id;
            m.Mesajlar = function.Add_Update<SCHOOL>(m.Mesajlar, "School/School_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "School", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<SCHOOL> m = new Mesajlar<SCHOOL>();
            m = function.Get<SCHOOL>(m, "School/School_Select?SchoolID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<SCHOOL> mb)
        {
            mb = function.Get<SCHOOL>(mb, "School/School_Delete?SchoolID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "School", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            
            Mesajlar<SCHOOL> m = new Mesajlar<SCHOOL>();
            m = function.Get<SCHOOL>(m, "School/School_Select?SchoolID=" + id);
            SchoolViewModel<SCHOOL> schoolViewModel = new SchoolViewModel<SCHOOL>();
            
            ViewBag.NameSurname = needs.NameSurname;
            schoolViewModel.Mesajlar = m;
            Mesajlar<EDUCATIONAL_INSTITUTION> mesajlar = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            mesajlar = function.Get<EDUCATIONAL_INSTITUTION>(mesajlar, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + m.Nesne.EducationID);
            schoolViewModel.Mesajlar.Nesne.Education = mesajlar.Nesne;
            return View(schoolViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            m = function.Get<EDUCATIONAL_INSTITUTION>(m, "EducationalInstitution/EducationalInstitution_List");
            Mesajlar<SCHOOL> mesajlar = new Mesajlar<SCHOOL>();
            mesajlar = function.Get<SCHOOL>(mesajlar, "School/School_SelectRelational?SchoolID=" + id);
            
            SchoolViewModel<SCHOOL> schoolViewModel = new SchoolViewModel<SCHOOL>()
            {
                List = new SelectList(m.Liste, "ObjectID", "EducationalName"),
                Id = mesajlar.Nesne.EducationID,
            };

            schoolViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(schoolViewModel);
        }
        [HttpPost]
        public IActionResult Edit(SchoolViewModel<SCHOOL> m)
        {
            m.Mesajlar = function.Add_Update<SCHOOL>(m.Mesajlar, "School/School_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
    }
}
