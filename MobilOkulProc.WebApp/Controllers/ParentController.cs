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
    public class ParentController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<PARENT> mb)
        {
            ParentViewModel<PARENT> m = new ParentViewModel<PARENT>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<PARENT>(mb, "Parent/Parent_List");    
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<USER>(User, "User/User_Select?USERID=" + item.UserID);
                item.User = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.FullName.ToLower().Contains(Search)).ToList();
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

            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            ParentViewModel<PARENT> viewModel = new ParentViewModel<PARENT>()
            {
                UserList = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                UserID = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ParentViewModel<PARENT> m)
        {
            m.Mesajlar.Nesne.UserID = m.UserID;
            m.Mesajlar = function.Add_Update<PARENT>(m.Mesajlar, "Parent/Parent_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Parent", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<PARENT> m = new Mesajlar<PARENT>();
            m = function.Get<PARENT>(m, "Parent/Parent_Select?ParentID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<PARENT> mb)
        {
            mb = function.Get<PARENT>(mb, "Parent/Parent_Delete?ParentID=" + mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Parent", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<PARENT> m = new Mesajlar<PARENT>();
            m = function.Get<PARENT>(m, "Parent/Parent_Select?ParentID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<PARENT> m = new Mesajlar<PARENT>();
            m = function.Get<PARENT>(m, "Parent/Parent_Select?ParentID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<PARENT> m)
        {
            m = function.Add_Update<PARENT>(m, "Parent/Parent_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
    }
}
