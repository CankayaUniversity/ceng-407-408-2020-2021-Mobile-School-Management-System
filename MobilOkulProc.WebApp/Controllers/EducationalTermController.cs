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
namespace MobilOkulProc.WebApp.Controllers
{
    public class EducationalTermController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<EDUCATIONAL_TERM> mb)
        {
            EducationalTermListViewModel<EDUCATIONAL_TERM> m = new EducationalTermListViewModel<EDUCATIONAL_TERM>();
            ViewBag.NameSurname = needs.NameSurname;

            m.Mesajlar = function.Get<EDUCATIONAL_TERM>(mb, "EducationalTerm/EducationalTerm_List");
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.EducationTerm.ToLower().Contains(Search)).ToList();
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
            ViewBag.NameSurname = needs.NameSurname;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Mesajlar<EDUCATIONAL_TERM> m)
        {
            m = function.Add_Update<EDUCATIONAL_TERM>(m, "EducationalTerm/EducationalTerm_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<EDUCATIONAL_TERM> m = new Mesajlar<EDUCATIONAL_TERM>();
            m = function.Get<EDUCATIONAL_TERM>(m, "EducationalTerm/EducationalTerm_Select?EducationalTermID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<EDUCATIONAL_TERM> mb)
        {
            mb = function.Get<EDUCATIONAL_TERM>(mb, "EducationalTerm/EducationalTerm_Delete?EducationalTermID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "EducationalTerm", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<EDUCATIONAL_TERM> m = new Mesajlar<EDUCATIONAL_TERM>();
            m = function.Get<EDUCATIONAL_TERM>(m, "EducationalTerm/EducationalTerm_Select?EducationalTermID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<EDUCATIONAL_TERM> m = new Mesajlar<EDUCATIONAL_TERM>();
            m = function.Get<EDUCATIONAL_TERM>(m, "EducationalTerm/EducationalTerm_Select?EducationalTermID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<EDUCATIONAL_TERM> m)
        {
            m = function.Add_Update<EDUCATIONAL_TERM>(m, "EducationalTerm/EducationalTerm_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
    }
}
