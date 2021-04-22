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
    public class EmployeeTypeController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<EMPLOYEE_TYPE> mb)
        {
            EmployeeTypeListViewModel<EMPLOYEE_TYPE> m = new EmployeeTypeListViewModel<EMPLOYEE_TYPE>();
            ViewBag.NameSurname = needs.NameSurname;

            m.Mesajlar = function.Get<EMPLOYEE_TYPE>(mb, "EmployeeType/EmployeeType_List");
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.EmployeeType.ToLower().Contains(Search)).ToList();
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
        public IActionResult Add(Mesajlar<EMPLOYEE_TYPE> m)
        {
            m = function.Add_Update<EMPLOYEE_TYPE>(m, "EmployeeType/EmployeeType_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<EMPLOYEE_TYPE> m = new Mesajlar<EMPLOYEE_TYPE>();
            m = function.Get<EMPLOYEE_TYPE>(m, "EmployeeType/EmployeeType_Select?EmployeeTypeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<EMPLOYEE_TYPE> mb)
        {
            mb = function.Get<EMPLOYEE_TYPE>(mb, "EmployeeType/EmployeeType_Delete?EmployeeTypeID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "EmployeeType", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<EMPLOYEE_TYPE> m = new Mesajlar<EMPLOYEE_TYPE>();
            m = function.Get<EMPLOYEE_TYPE>(m, "EmployeeType/EmployeeType_Select?EmployeeTypeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<EMPLOYEE_TYPE> m = new Mesajlar<EMPLOYEE_TYPE>();
            m = function.Get<EMPLOYEE_TYPE>(m, "EmployeeType/EmployeeType_Select?EmployeeTypeID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<EMPLOYEE_TYPE> m)
        {
            m = function.Add_Update<EMPLOYEE_TYPE>(m, "EmployeeType/EmployeeType_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }

    }
}
