using Microsoft.AspNetCore.Mvc;
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
    public class CityController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<CITY> mb)
        {
            CityListViewModel<CITY> m = new CityListViewModel<CITY>();
            ViewBag.NameSurname = needs.NameSurname;

            m.Mesajlar = function.Get<CITY>(mb, "City/City_List");
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.CityName.ToLower().Contains(Search)).ToList();
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
        public IActionResult Add(Mesajlar<CITY> m)
        {
            m = function.Add_Update<CITY>(m, "City/City_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<CITY> m = new Mesajlar<CITY>();
            m = function.Get<CITY>(m, "City/City_Select?CITYID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<CITY> mb)
        {
            mb = function.Get<CITY>(mb, "City/City_Delete?CityID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "City", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<CITY> m = new Mesajlar<CITY>();
            m = function.Get<CITY>(m, "City/City_Select?CityID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<CITY> m = new Mesajlar<CITY>();
            m = function.Get<CITY>(m, "City/City_Select?CityID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<CITY> m)
        {
            m = function.Add_Update<CITY>(m, "City/City_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
    }
}
