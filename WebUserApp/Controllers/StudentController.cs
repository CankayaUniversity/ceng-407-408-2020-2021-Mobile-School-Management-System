using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Announcements()
        {
            return View();
        }
        public IActionResult Teachers()
        {
            return View();
        }
        public IActionResult Notes()
        {
            return View();
        }
        public IActionResult Exams()
        {
            return View();
        }
        public IActionResult Lectures()
        {
            return View();
        }

    }
}
