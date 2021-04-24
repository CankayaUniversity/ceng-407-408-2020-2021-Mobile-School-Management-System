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
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.FullName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            ParentViewModel<PARENT> viewModel = new ParentViewModel<PARENT>()
            {
                List = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ParentViewModel<PARENT> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<PARENT>(m.Mesajlar, "Parent/Parent_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Parent", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            ParentViewModel<PARENT> m = new ParentViewModel<PARENT>();
            m.Mesajlar = function.Get<PARENT>(m.Mesajlar, "Parent/Parent_SelectRelational?ParentID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ParentViewModel<PARENT> mb)
        {
            mb.Mesajlar = function.Get<PARENT>(mb.Mesajlar, "Parent/Parent_Delete?ParentID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Parent", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<PARENT> m = new Mesajlar<PARENT>();
            m = function.Get<PARENT>(m, "Parent/Parent_Select?ParentID=" + id);
            ParentViewModel<PARENT> ParentViewModel = new ParentViewModel<PARENT>();

            ViewBag.NameSurname = needs.NameSurname;
            ParentViewModel.Mesajlar = m;
            Mesajlar<USER> mesajlar = new Mesajlar<USER>();
            mesajlar = function.Get<USER>(mesajlar, "User/User_Select?UserID=" + m.Nesne.UserID);
            ParentViewModel.Mesajlar.Nesne.User = mesajlar.Nesne;
            return View(ParentViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            Mesajlar<PARENT> mesajlar = new Mesajlar<PARENT>();
            mesajlar = function.Get<PARENT>(mesajlar, "Parent/Parent_SelectRelational?ParentID=" + id);
            ParentViewModel<PARENT> ParentViewModel = new ParentViewModel<PARENT>()
            {
                List = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                SelectedId = mesajlar.Nesne.UserID
            };

            ParentViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(ParentViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ParentViewModel<PARENT> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<PARENT>(m.Mesajlar, "Parent/Parent_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Parent", m);
        }
    }
}
