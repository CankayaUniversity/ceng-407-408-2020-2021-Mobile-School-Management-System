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
    public class TeacherController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<TEACHER> mb)
        {
            TeacherViewModel<TEACHER> m = new TeacherViewModel<TEACHER>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            Mesajlar<BRANCH> Branch = new Mesajlar<BRANCH>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<TEACHER>(mb, "Teacher/Teacher_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
                Branch = function.Get<BRANCH>(Branch, "Branch/Branch_Select?BranchID=" + item.BranchID);
                item.Branch = Branch.Nesne;
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
            Mesajlar<BRANCH> b = new Mesajlar<BRANCH>();
            b = function.Get<BRANCH>(b, "Branch/Branch_List");
            TeacherViewModel<TEACHER> viewModel = new TeacherViewModel<TEACHER>()
            {
                UserList = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                BranchList = new SelectList(b.Liste,"ObjectID","BranchName"),
                BranchId = -1,
                UserId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TeacherViewModel<TEACHER> m)
        {
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar.Nesne.BranchID = m.BranchId;
            m.Mesajlar = function.Add_Update<TEACHER>(m.Mesajlar, "Teacher/Teacher_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Teacher", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            TeacherViewModel<TEACHER> m = new TeacherViewModel<TEACHER>();
            m.Mesajlar = function.Get<TEACHER>(m.Mesajlar, "Teacher/Teacher_SelectRelational?TeacherID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TeacherViewModel<TEACHER> mb)
        {
            mb.Mesajlar = function.Get<TEACHER>(mb.Mesajlar, "Teacher/Teacher_Delete?TeacherID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Teacher", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<TEACHER> m = new Mesajlar<TEACHER>();
            m = function.Get<TEACHER>(m, "Teacher/Teacher_Select?TeacherID=" + id);
            TeacherViewModel<TEACHER> TeacherViewModel = new TeacherViewModel<TEACHER>();

            ViewBag.NameSurname = needs.NameSurname;
            TeacherViewModel.Mesajlar = m;
            Mesajlar<USER> mesajlar = new Mesajlar<USER>();
            Mesajlar<BRANCH> branch = new Mesajlar<BRANCH>();
            mesajlar = function.Get<USER>(mesajlar, "User/User_Select?UserID=" + m.Nesne.UserID);
            branch = function.Get<BRANCH>(branch,"Branch/Branch_Select?BranchID="+m.Nesne.BranchID);
            TeacherViewModel.Mesajlar.Nesne.User = mesajlar.Nesne;
            TeacherViewModel.Mesajlar.Nesne.Branch = branch.Nesne;
            return View(TeacherViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            Mesajlar<BRANCH> b = new Mesajlar<BRANCH>();
            m = function.Get<USER>(m, "User/User_List");
            b = function.Get<BRANCH>(b, "Branch/Branch_List");

            Mesajlar<TEACHER> teacher = new Mesajlar<TEACHER>();
            teacher = function.Get<TEACHER>(teacher, "Teacher/Teacher_SelectRelational?TeacherID=" + id);
            TeacherViewModel<TEACHER> TeacherViewModel = new TeacherViewModel<TEACHER>()
            {
                UserList = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                BranchList = new SelectList(b.Liste,"ObjectID","BranchName"),
                BranchId = teacher.Nesne.BranchID,
                UserId = teacher.Nesne.UserID,
            };

            TeacherViewModel.Mesajlar = teacher;
            

            ViewBag.NameSurname = needs.NameSurname;
            return View(TeacherViewModel);
        }
        [HttpPost]
        public IActionResult Edit(TeacherViewModel<TEACHER> m)
        {
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar.Nesne.BranchID = m.BranchId;
            m.Mesajlar = function.Add_Update<TEACHER>(m.Mesajlar, "Teacher/Teacher_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Teacher", m);
        }
    }
}
