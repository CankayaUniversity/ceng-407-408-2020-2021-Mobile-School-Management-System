

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using static MobilOkulProc.MobileApp.Controllers.HomePageController;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class FeedBackPageController : Controller
    {
        public IActionResult FeedBackPage()
        {
            ViewBag.NameSurname = needs.NameSurname;
            return View();
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


    }
       
}
