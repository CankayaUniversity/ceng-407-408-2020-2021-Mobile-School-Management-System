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
    public class FeedbackController: Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<FEEDBACK> mb)
        {
            FeedbackViewModel<FEEDBACK> m = new FeedbackViewModel<FEEDBACK>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<FEEDBACK>(mb, "Feedback/Feedback_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.FeedbackContent.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            FeedbackViewModel<FEEDBACK> viewModel = new FeedbackViewModel<FEEDBACK>()
            {
                List = new SelectList(m.Liste, "ObjectID", "FullName"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FeedbackViewModel<FEEDBACK> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Feedback", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            FeedbackViewModel<FEEDBACK> m = new FeedbackViewModel<FEEDBACK>();
            m.Mesajlar = function.Get<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_SelectRelational?FeedbackID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FeedbackViewModel<FEEDBACK> mb)
        {
            mb.Mesajlar = function.Get<FEEDBACK>(mb.Mesajlar, "Feedback/Feedback_Delete?FeedbackID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "Feedback", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<FEEDBACK> m = new Mesajlar<FEEDBACK>();
            m = function.Get<FEEDBACK>(m, "Feedback/Feedback_Select?FeedbackID=" + id);
            FeedbackViewModel<FEEDBACK> FeedbackViewModel = new FeedbackViewModel<FEEDBACK>();

            ViewBag.NameSurname = needs.NameSurname;
            FeedbackViewModel.Mesajlar = m;
            Mesajlar<USER> mesajlar = new Mesajlar<USER>();
            mesajlar = function.Get<USER>(mesajlar, "User/User_Select?UserID=" + m.Nesne.UserID);
            FeedbackViewModel.Mesajlar.Nesne.User = mesajlar.Nesne;
            return View(FeedbackViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            Mesajlar<FEEDBACK> mesajlar = new Mesajlar<FEEDBACK>();
            mesajlar = function.Get<FEEDBACK>(mesajlar, "Feedback/Feedback_SelectRelational?FeedbackID=" + id);
            FeedbackViewModel<FEEDBACK> FeedbackViewModel = new FeedbackViewModel<FEEDBACK>()
            {
                List = new SelectList(m.Liste, "ObjectID", "FullName"),
                SelectedId = mesajlar.Nesne.UserID
            };

            FeedbackViewModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(FeedbackViewModel);
        }
        [HttpPost]
        public IActionResult Edit(FeedbackViewModel<FEEDBACK> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Feedback", m);
        }

    }
}
