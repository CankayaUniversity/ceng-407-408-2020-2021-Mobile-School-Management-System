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
using MobilOkulProc.MobileApp.Models;
using Microsoft.Extensions.Configuration;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class FeedBackPageController : Controller
    {

        public static Functions function = new Functions();
        public static Needs needs = new Needs();


        public FeedBackPageController(IConfiguration cfg)
        {
            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
        }

        public IActionResult FeedBackPage(USER user)
        {

            if (user.NameSurname != null)
            {
                needs.NameSurname = user.NameSurname;

            }
            ViewBag.NameSurname = needs.NameSurname;
            return View();
        }

       
        public IActionResult List(string Search, int? page, Mesajlar<FEEDBACK> mb)
        {
            FeedbackPageModel<FEEDBACK> m = new FeedbackPageModel<FEEDBACK>();
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
            FeedbackPageModel<FEEDBACK> viewModel = new FeedbackPageModel<FEEDBACK>()
            {
                List = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                SelectedId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FeedbackPageModel<FEEDBACK> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Feedback", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            FeedbackPageModel<FEEDBACK> m = new FeedbackPageModel<FEEDBACK>();
            m.Mesajlar = function.Get<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_SelectRelational?FeedbackID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FeedbackPageModel<FEEDBACK> mb)
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
            FeedbackPageModel<FEEDBACK> FeedbackModel = new FeedbackPageModel<FEEDBACK>();

            ViewBag.NameSurname = needs.NameSurname;
            FeedbackModel.Mesajlar = m;
            Mesajlar<USER> mesajlar = new Mesajlar<USER>();
            mesajlar = function.Get<USER>(mesajlar, "User/User_Select?UserID=" + m.Nesne.UserID);
            FeedbackModel.Mesajlar.Nesne.User = mesajlar.Nesne;
            return View(FeedbackModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            Mesajlar<FEEDBACK> mesajlar = new Mesajlar<FEEDBACK>();
            mesajlar = function.Get<FEEDBACK>(mesajlar, "Feedback/Feedback_SelectRelational?FeedbackID=" + id);
            FeedbackPageModel<FEEDBACK> FeedbackModel = new FeedbackPageModel<FEEDBACK>()
            {
                List = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                SelectedId = mesajlar.Nesne.UserID
            };

            FeedbackModel.Mesajlar = mesajlar;


            ViewBag.NameSurname = needs.NameSurname;
            return View(FeedbackModel);
        }
        [HttpPost]
        public IActionResult Edit(FeedbackPageModel<FEEDBACK> m)
        {
            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar = function.Add_Update<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "Feedback", m);
        }

    }
}
