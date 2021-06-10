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
    public class GradeTypeController : Controller
    {


        public IActionResult List(string Search, int? page, Mesajlar<GRADETYPE> mb)
        {
            GradeTypeViewModel<GRADETYPE> m = new GradeTypeViewModel<GRADETYPE>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<GRADETYPE>(mb, "GradeType/GradeType_List");
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.GradeName.ToLower().Contains(Search)).ToList();
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
        public IActionResult Add(Mesajlar<GRADETYPE> m)
        {
            m = function.Add_Update<GRADETYPE>(m, "GradeType/GradeType_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<GRADETYPE> m = new Mesajlar<GRADETYPE>();
            m = function.Get<GRADETYPE>(m, "GradeType/GradeType_Select?GradeTypeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<GRADETYPE> mb)
        {
            mb = function.Get<GRADETYPE>(mb, "GradeType/GradeType_Delete?GradeTypeID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "GradeType", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<GRADETYPE> m = new Mesajlar<GRADETYPE>();
            m = function.Get<GRADETYPE>(m, "GradeType/GradeType_Select?GradeTypeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<GRADETYPE> m = new Mesajlar<GRADETYPE>();
            m = function.Get<GRADETYPE>(m, "GradeType/GradeType_Select?GradeTypeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<GRADETYPE> m)
        {
            m = function.Add_Update<GRADETYPE>(m, "GradeType/GradeType_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }

    }
}
