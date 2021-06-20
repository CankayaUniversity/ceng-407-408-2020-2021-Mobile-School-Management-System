using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class TeacherController : Controller
    {
        private IMemoryCache _cache;
        public TeacherController(IConfiguration cfg, IMemoryCache memoryCache)
        {

            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
            _cache = memoryCache;

        }
        public IActionResult Get(int Id)
        {
            var teacherList = _cache.Get("Teachers") as List<TEACHER>;
            TEACHER teacher = teacherList.Where(m => m.ObjectID == Id).FirstOrDefault();
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            return View(teacher);
        }
        public async Task<IActionResult> Notes()
        {
            Mesajlar<LECTURE> m = new Mesajlar<LECTURE>();
            m = await functions.Get<LECTURE>(m, "Lecture/Lecture_List");
            Mesajlar<GRADETYPE> g = new Mesajlar<GRADETYPE>();
            g = await functions.Get<GRADETYPE>(g, "GradeType/GradeType_List");
            Mesajlar<STUDENT> s = new Mesajlar<STUDENT>();
            s = await functions.Get<STUDENT>(s, "Student/Student_List");
            GradeViewModel<GRADE> viewModel = new GradeViewModel<GRADE>()
            {
                List = new SelectList(m.Liste, "ObjectID", "LectureName"),
                List2 = new SelectList(g.Liste, "ObjectID", "GradeName"),
                List3 = new SelectList(s.Liste,"ObjectID","FullName"),
                SelectedId = -1,
                SelectedId2 = -1,
                SelectedId3 = -1
            };


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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Notes(GradeViewModel<GRADE> m)
        {
            m.Mesajlar.Nesne.LectureID = m.SelectedId;
            m.Mesajlar.Nesne.GradeTypeID = m.SelectedId2;
            m.Mesajlar.Nesne.StudentID = m.SelectedId3;
            m.Mesajlar = await functions.Add_Update<GRADE>(m.Mesajlar, "Grade/Grade_Insert");


            Mesajlar<LECTURE> m1 = new Mesajlar<LECTURE>();
            m1 = await functions.Get<LECTURE>(m1, "Lecture/Lecture_List");
            Mesajlar<GRADETYPE> g = new Mesajlar<GRADETYPE>();
            g = await functions.Get<GRADETYPE>(g, "GradeType/GradeType_List");
            Mesajlar<STUDENT> s = new Mesajlar<STUDENT>();
            s = await functions.Get<STUDENT>(s, "Student/Student_List");
            GradeViewModel<GRADE> viewModel = new GradeViewModel<GRADE>()
            {
                List = new SelectList(m1.Liste, "ObjectID", "LectureName"),
                List2 = new SelectList(g.Liste, "ObjectID", "GradeName"),
                List3 = new SelectList(s.Liste, "ObjectID", "FullName"),
                SelectedId = -1,
                SelectedId2 = -1,
                SelectedId3 = -1
            };
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion

            ViewBag.Result = "Not başarıyla eklendi";
            ViewBag.Status = "success";
            return View(viewModel);
        }
    }
}
