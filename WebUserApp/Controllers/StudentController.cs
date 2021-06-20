using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebUserApp.Controllers.HomeController;

namespace MobilOkulProc.WebUserApp.Controllers
{
    public class StudentController : Controller
    {
        private IMemoryCache _cache;
        public StudentController(IConfiguration cfg, IMemoryCache memoryCache)
        {

            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
            _cache = memoryCache;

        }
        public IActionResult Announcements()
        {

            var news = _cache.Get("News") as List<NEWS>;



            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(news);
        }
        public IActionResult Teachers()
        {
            var teachers = _cache.Get("Teachers") as List<TEACHER>;

            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(teachers);
        }
        public IActionResult Notes()
        {

            List<GRADE> grades = _cache.Get("Grades") as List<GRADE>;

            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(grades);
        }
        public IActionResult Exams()
        {
            List<EXAM> exams = _cache.Get("Exam") as List<EXAM>;
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(exams);
        }
        public async Task<IActionResult> Lectures()
        {
            Mesajlar<LECTURE> Lectures = new Mesajlar<LECTURE>();
            Lectures = await functions.Get<LECTURE>(Lectures, "Lecture/Lecture_ListRelational");



            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(Lectures.Liste);
        }

    }
}
