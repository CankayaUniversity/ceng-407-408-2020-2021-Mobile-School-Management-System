using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebApp.Controllers
{
    public class SyllabusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
