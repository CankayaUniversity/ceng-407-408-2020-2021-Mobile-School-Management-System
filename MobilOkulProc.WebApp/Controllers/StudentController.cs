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
    public class StudentController : Controller
    {
        
        public IActionResult List(string Search, int? page, Mesajlar<STUDENT> mb)
        {
            StudentViewModel<STUDENT> m = new StudentViewModel<STUDENT>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<STUDENT>(mb, "Student/Student_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.StdName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            StudentViewModel<STUDENT> viewModel = new StudentViewModel<STUDENT>()
            {
                List = new SelectList(m.Liste, "ObjectID", "FullName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(StudentViewModel<STUDENT> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<STUDENT>(m.Mesajlar, "Student/Student_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Student", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            StudentViewModel<STUDENT> m = new StudentViewModel<STUDENT>();
            m.Mesajlar = function.Get<STUDENT>(m.Mesajlar, "Student/Student_SelectRelational?StudentID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(StudentViewModel<STUDENT> mb)
        {
            mb.Mesajlar = function.Get<STUDENT>(mb.Mesajlar, "Student/Student_Delete?StudentID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Student", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<STUDENT> m = new Mesajlar<STUDENT>();
            m = function.Get<STUDENT>(m, "Student/Student_Select?StudentID=" + id);
            StudentViewModel<STUDENT> StudentViewModel = new StudentViewModel<STUDENT>();

            ViewBag.NameSurname = needs.NameSurname;
            StudentViewModel.Mesajlar = m;
            Mesajlar<USER> mesajlar = new Mesajlar<USER>();
            mesajlar = function.Get<USER>(mesajlar, "User/User_Select?UserID=" + m.Nesne.UserID);
            StudentViewModel.Mesajlar.Nesne.User = mesajlar.Nesne;
            return View(StudentViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            Mesajlar<STUDENT> mesajlar = new Mesajlar<STUDENT>();
            mesajlar = function.Get<STUDENT>(mesajlar, "Student/Student_SelectRelational?StudentID=" + id);
            StudentViewModel<STUDENT> StudentViewModel = new StudentViewModel<STUDENT>()
            {
                List = new SelectList(m.Liste, "ObjectID", "FullName"),
                SelectedId = mesajlar.Nesne.UserID
            };

            StudentViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(StudentViewModel);
        }
        [HttpPost]
        public IActionResult Edit(StudentViewModel<STUDENT> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<STUDENT>(m.Mesajlar, "Student/Student_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Student", m);
        }
    }
}
