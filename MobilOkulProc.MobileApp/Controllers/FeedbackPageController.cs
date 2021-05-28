using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using System;
using System.Linq;
using X.PagedList;
using static MobilOkulProc.MobileApp.Controllers.HomePageController;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class FeedbackPageController : Controller
    {
        public IActionResult FeedbackPage()
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = HttpContext.Session.GetString("no");
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            return View();
        }


        public IActionResult Add()
        {

            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = HttpContext.Session.GetString("no");
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

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
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = HttpContext.Session.GetString("no");
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            m.Mesajlar.Nesne.UserID = m.SelectedId;
            m.Mesajlar.Nesne.FeedbackDate = DateTime.Now;
            m.Mesajlar.Nesne.FeedbackType = 1;
            m.Mesajlar.Nesne.Status = true;

            m.Mesajlar = function.Add_Update<FEEDBACK>(m.Mesajlar, "Feedback/Feedback_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "FeedbackPage", m.Mesajlar);
        }
        public IActionResult List(string Search, int? page, Mesajlar<FEEDBACK> mb)
        {
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = HttpContext.Session.GetString("no");
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");

            FeedbackPageModel<FEEDBACK> m = new FeedbackPageModel<FEEDBACK>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<FEEDBACK>(mb, "Feedback/Feedback_List");

            ViewBag.Userno= int.Parse(HttpContext.Session.GetString("no"));

  

            foreach (var item in m.Mesajlar.Liste)
            {
 
                    User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                    item.User = User.Nesne;
 
            }

                m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
                return View(m);
            
        }


    }
       
}
