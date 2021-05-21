

using Microsoft.AspNetCore.Mvc;
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

       

    }
       
}
