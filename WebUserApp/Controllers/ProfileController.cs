using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebUserApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebUserApp.Controllers.HomeController;

namespace MobilOkulProc.WebUserApp.Controllers
{
    public class ProfileController : Controller
    {
        private IMemoryCache _cache;
        public ProfileController(IConfiguration cfg, IMemoryCache memoryCache)
        {

            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
            _cache = memoryCache;

        }
        public async Task<IActionResult> Profile()
        {
            Mesajlar<STUDENT> student = new Mesajlar<STUDENT>();
            student = await functions.Get<STUDENT>(student, "Student/Student_Select?StudentID=" + needs.UserID);
            Mesajlar<STUDENT_PARENT> studentParent = new Mesajlar<STUDENT_PARENT>();
            studentParent = await functions.Get<STUDENT_PARENT>(studentParent, "StudentParent/StudentParent_ListRelationalStudent?StudentID=" + student.Nesne.ObjectID);
            ProfileViewModel viewModel = new ProfileViewModel();
            viewModel.Student = student.Nesne;
            viewModel.StudentParentList = studentParent.Liste;

            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(viewModel);
        }
    }
}
