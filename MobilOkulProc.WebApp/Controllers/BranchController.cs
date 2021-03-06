﻿using System;
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
    public class BranchController : Controller
    {


        public IActionResult List(string Search, int? page, Mesajlar<BRANCH> mb)
        {
            BranchListViewModel<BRANCH> m = new BranchListViewModel<BRANCH>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<BRANCH>(mb, "Branch/Branch_List");
            if (Search != null)
            {
               m.Mesajlar.Liste =  m.Mesajlar.Liste.Where(m => m.BranchName.ToLower().Contains(Search)).ToList();
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
        public IActionResult Add(Mesajlar<BRANCH> m)
        {
            m = function.Add_Update<BRANCH>(m, "Branch/Branch_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Delete(int id)
        {
            Mesajlar<BRANCH> m = new Mesajlar<BRANCH>();
            m = function.Get<BRANCH>(m,"Branch/Branch_Select?BranchID="+id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Mesajlar<BRANCH> mb)
        {
            mb = function.Get<BRANCH>(mb, "Branch/Branch_Delete?BranchID="+mb.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Branch", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {
            Mesajlar<BRANCH> m = new Mesajlar<BRANCH>();
            m = function.Get<BRANCH>(m, "Branch/Branch_Select?BranchID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<BRANCH> m = new Mesajlar<BRANCH>();
            m = function.Get<BRANCH>(m, "Branch/Branch_Select?BranchID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(Mesajlar<BRANCH> m)
        {
            m = function.Add_Update<BRANCH>(m, "Branch/Branch_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        
    }
}
