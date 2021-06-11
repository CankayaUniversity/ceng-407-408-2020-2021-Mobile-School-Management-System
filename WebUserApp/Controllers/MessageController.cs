using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Mailbox()
        {
            return View();
        }
        public IActionResult Compose()
        {
            return View();
        }
        public IActionResult Read()
        {
            return View();
        }
    }
}
