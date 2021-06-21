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

        public async Task<IActionResult> Announcements()
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            EdIns = await functions.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_List");
            School = await functions.Get<SCHOOL>(School, "School/School_List");
            User = await functions .Get<USER>(User, "User/User_Select?UserID=" + needs.UserID);
            List<USER> usrList = new List<USER>();
            usrList.Add(User.Nesne);
            User.Liste = usrList;
            NewsViewModel<NEWS> viewModel = new NewsViewModel<NEWS>()
            {
                EducationalInstitutionList = new SelectList(EdIns.Liste, "ObjectID", "EducationalName"),
                SchoolList = new SelectList(School.Liste, "ObjectID", "SchoolName"),
                UserList = new SelectList(User.Liste, "ObjectID", "FullName"),
                EducationalInstitutionId = -1,
                SchoolId = -1,
                UserId = -1,
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
        public async Task<IActionResult> Announcements(NewsViewModel<NEWS> m)
        {
            m.Mesajlar.Nesne.EducationID = m.EducationalInstitutionId;
            m.Mesajlar.Nesne.SchoolID = m.SchoolId;
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar = await functions.Add_Update<NEWS>(m.Mesajlar, "News/News_Insert");
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            EdIns = await functions.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_List");
            School = await functions.Get<SCHOOL>(School, "School/School_List");
            User = await functions.Get<USER>(User, "User/User_List");
            NewsViewModel<NEWS> viewModel = new NewsViewModel<NEWS>()
            {
                EducationalInstitutionList = new SelectList(EdIns.Liste, "ObjectID", "EducationalName"),
                SchoolList = new SelectList(School.Liste, "ObjectID", "SchoolName"),
                UserList = new SelectList(User.Liste, "ObjectID", "FullName"),
                EducationalInstitutionId = -1,
                SchoolId = -1,
                UserId = -1,
            };
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            ViewBag.Result = "Duyuru başarıyla eklendi";
            ViewBag.Status = "success";
            return View(viewModel);
        }
        public async Task<IActionResult> Exams()
        {
            Mesajlar<CLASS_SECTION> m = new Mesajlar<CLASS_SECTION>();
            m = await functions.Get<CLASS_SECTION>(m, "ClassSection/ClassSection_List");
            Mesajlar<TEACHER> t = new Mesajlar<TEACHER>();
            t = await functions.Get<TEACHER>(t, "Teacher/Teacher_SelectRelationalUser?UserID=" + needs.UserID);
            Mesajlar<LECTURE> l = new Mesajlar<LECTURE>();
            l = await functions.Get<LECTURE>(l, "Lecture/Lecture_ListTeacher?TeacherID=" + t.Nesne.ObjectID);
            ExamViewModel<EXAM> viewModel = new ExamViewModel<EXAM>()
            {
                List = new SelectList(m.Liste, "ObjectID", "ClassSectionName"),
                List2 = new SelectList(l.Liste,"ObjectID", "LectureName"),
                SelectedId2 = -1,
                SelectedId = -1
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
        public async Task<IActionResult> Exams(ExamViewModel<EXAM> m)
        {
            m.Mesajlar.Nesne.ClassSectionsID = m.SelectedId;
            m.Mesajlar.Nesne.LectureID = m.SelectedId2;
            m.Mesajlar = await functions.Add_Update<EXAM>(m.Mesajlar, "Exam/Exam_Insert");
            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion
            Mesajlar<CLASS_SECTION> m1 = new Mesajlar<CLASS_SECTION>();
            m1 = await functions.Get<CLASS_SECTION>(m1, "ClassSection/ClassSection_List");
            Mesajlar<TEACHER> t = new Mesajlar<TEACHER>();
            t = await functions.Get<TEACHER>(t, "Teacher/Teacher_SelectRelationalUser?UserID=" + needs.UserID);
            Mesajlar<LECTURE> l = new Mesajlar<LECTURE>();
            l = await functions.Get<LECTURE>(l, "Lecture/Lecture_ListTeacher?TeacherID=" + t.Nesne.ObjectID);
            ExamViewModel<EXAM> viewModel = new ExamViewModel<EXAM>()
            {
                List = new SelectList(m1.Liste, "ObjectID", "ClassSectionName"),
                List2 = new SelectList(l.Liste, "ObjectID", "LectureName"),
                SelectedId2 = -1,
                SelectedId = -1
            };
            ViewBag.Result = "Sınav başarıyla oluşturuldu.";
            ViewBag.Status = "success";
            return View(viewModel);
        }
    }
}
