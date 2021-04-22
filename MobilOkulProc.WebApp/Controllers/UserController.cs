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
    public class UserController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<USER> mb)
        {
            UserListViewModel<USER> m = new UserListViewModel<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<USER>(mb, "User/User_List");
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.NameSurname.ToLower().Contains(Search)).ToList();
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
        public IActionResult Add(Mesajlar<USER> m)
        {
            m = function.Add_Update<USER>(m, "User/User_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_Select?UserID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<USER> mb)
        {
            mb = function.Get<USER>(mb, "User/User_Delete?UserID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "User", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_Select?UserID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_Select?UserID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<USER> m)
        {
            m = function.Add_Update<USER>(m, "User/User_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
    }
}
