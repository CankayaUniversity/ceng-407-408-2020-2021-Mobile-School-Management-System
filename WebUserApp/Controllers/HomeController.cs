using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebUserApp.ViewModels;
using Newtonsoft.Json;
using WebUserApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUserApp.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        private readonly ILogger<HomeController> _logger;
        public static Needs needs = new Needs();
        public static Functions functions = new Functions();
        public HomeController(ILogger<HomeController> logger, IConfiguration cfg, IMemoryCache memoryCache)
        {
            _logger = logger;
            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
            _cache = memoryCache;
            
        }
        public async Task<IActionResult> WelcomeTeacher()
        {
            
            return View();
        }
        public async Task<IActionResult> ChooseChild()
        {
            Mesajlar<PARENT> parent = new Mesajlar<PARENT>();
            parent = await functions.Get<PARENT>(parent, "Parent/Parent_SelectUser?UserID=" + needs.UserID);
            Mesajlar<STUDENT_PARENT> m = new Mesajlar<STUDENT_PARENT>();
            m = await functions.Get<STUDENT_PARENT>(m, "StudentParent/StudentParent_ListRelationalParent?ParentID=" + parent.Nesne.ObjectID);
            List<STUDENT> students = new List<STUDENT>();
            foreach (var item in m.Liste)
            {
                students.Add(item.Student);
            }
            ParentViewModel viewModel = new ParentViewModel()
            {
                StudentList = new SelectList(students, "ObjectID", "FullName"),
                SelectedId = -1,
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ChooseChild(ParentViewModel viewModel)
        {
            needs.UserID = viewModel.SelectedId;
            return RedirectToAction("Welcome","Home");
        }
        public async Task<IActionResult> Welcome()
        {
            #region Initializations
            HomeViewModel homeViewModel = new HomeViewModel();
            Mesajlar<STUDENT_CLASS> studentClass = new Mesajlar<STUDENT_CLASS>();
            Mesajlar<CLASS_SECTION> classSection = new Mesajlar<CLASS_SECTION>();
            Mesajlar<ABSENCE> absence = new Mesajlar<ABSENCE>();
            Mesajlar<LECTURE> lecture = new Mesajlar<LECTURE>();
            Mesajlar<SYLLABUS> syllabus = new Mesajlar<SYLLABUS>();
            Mesajlar<EXAM> exam = new Mesajlar<EXAM>();
            Mesajlar<GRADE> grade = new Mesajlar<GRADE>();
            Mesajlar<MESSAGE> messages = new Mesajlar<MESSAGE>();
            List<MESSAGE> LastFiveMessagesNotRead = new List<MESSAGE>();
            Mesajlar<NEWS> news = new Mesajlar<NEWS>();
            Mesajlar<SCHOOL_STUDENT> schoolStudent = new Mesajlar<SCHOOL_STUDENT>();
            Mesajlar<TEACHER_SCHOOL> teacherSchool = new Mesajlar<TEACHER_SCHOOL>();
            Mesajlar<TEACHER> teachers = new Mesajlar<TEACHER>();
            List<TEACHER> teachersList = new List<TEACHER>();
            double TotalAbsence = 0;
            int WeeklyLoad = 0;
            string MostRecentExam = "";
            int ExamsLeftCount = 0;
            string LatestGrade = "";

            #endregion


            if (!_cache.TryGetValue("TotalAbsence", out TotalAbsence))
            {


                #region Find User's Class, then it's Class Section with the ID = ClassSectionName ex: 10 - Fen - A
                studentClass = await functions.Get<STUDENT_CLASS>(studentClass, "StudentClass/StudentClass_SelectStudent?StudentID=" + needs.UserID);
                classSection = await functions.Get<CLASS_SECTION>(classSection, "ClassSection/ClassSection_Select?ObjectID=" + studentClass.Nesne.ClassSectionID);
                _cache.Set("ClassSectionName", classSection.Nesne.ClassSectionName);
                #endregion

                #region Calculate total absence of a student
                absence = await functions.Get<ABSENCE>(absence, "Absence/Absence_ListStudent?StudentID=" + needs.UserID);
                foreach (var item in absence.Liste)
                {
                    TotalAbsence += item.TotalAbsence;
                }
                _cache.Set("TotalAbsence", TotalAbsence);
                #endregion

                #region Calculate Weekly Load of a Student
                syllabus = await functions.Get<SYLLABUS>(syllabus, "Syllabus/Syllabus_ListRelationalClassSections?ClassSectionsID=" + studentClass.Nesne.ClassSectionID);

                foreach (var item in syllabus.Liste)
                {
                    if (item.Nine != null && item.Nine != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Ten != null && item.Ten != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Eleven != null && item.Eleven != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Twelwe != null && item.Twelwe != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Thirtheen != null && item.Thirtheen != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Fourteen != null && item.Fourteen != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Fifteen != null && item.Fifteen != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Sixteen != null && item.Sixteen != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }
                    if (item.Seventeen != null && item.Seventeen != "Öğle Tatili")
                    {
                        WeeklyLoad += 1;
                    }

                }
                _cache.Set("WeeklyLoad", WeeklyLoad);
                #endregion

                #region Syllabus
                syllabus = await functions.Get<SYLLABUS>(syllabus, "Syllabus/Syllabus_ListRelational");
                _cache.Set("Syllabus", syllabus.Liste);
                #endregion

                #region Exam List
                exam = await functions.Get<EXAM>(exam, "Exam/Exam_ListRelationalLecture?LectureID=" + studentClass.Nesne.ClassSectionID);
                _cache.Set("Exam", exam.Liste);
                #endregion

                #region MostRecentExam and Total Exam Counts
                EXAM MostRecentDateObject = exam.Liste[0];
                foreach (var item in exam.Liste)
                {

                    if (item.ExamDate < MostRecentDateObject.ExamDate && item.ExamDate > DateTime.Now)
                    {
                        MostRecentDateObject = item;

                    }
                    if (item.ExamDate > DateTime.Now)
                    {
                        ExamsLeftCount++;
                    }
                }
                if (MostRecentDateObject.ExamDate > DateTime.Now)
                {
                    MostRecentExam = MostRecentDateObject.ExamDate.ToString("dd.MM.yyyy - HH:mm") + " - " + MostRecentDateObject.Lecture.LectureName;
                    if (ExamsLeftCount == 0)
                    {
                        ExamsLeftCount++;
                    }
                }
                _cache.Set("ExamsLeftCount", ExamsLeftCount.ToString());
                _cache.Set("MostRecentExam", MostRecentExam);
                #endregion

                #region Latest Grade
                grade = await functions.Get<GRADE>(grade, "Grade/Grade_ListRelationalStudent?StudentID=" + needs.UserID);
                LatestGrade = grade.Liste[grade.Liste.Count - 1].Lecture.LectureName + " - " + grade.Liste[grade.Liste.Count - 1].Grade;
                _cache.Set("LatestGrade", LatestGrade);
                _cache.Set("Grades", grade.Liste);
                #endregion

                #region MessageCounts
                messages = await functions.Get<MESSAGE>(messages, "Messages/Message_ListRelationalReceiverNotRead?ReceiveID=" + needs.UserID);
                _cache.Set("Messages", messages.Liste);
                for (int i = messages.Liste.Count - 1, j = 0; j < 5; i--, j++)
                {
                    if (i >= 0)
                    {
                        LastFiveMessagesNotRead.Add(messages.Liste[i]);
                    }
                }
                needs.TotalNumberOfMessages = messages.Liste.Count.ToString();
                messages = await functions.Get<MESSAGE>(messages, "Messages/Message_ListRelationalReceiver?ReceiveID=" + needs.UserID);
                needs.LastFiveMessagesNotRead = LastFiveMessagesNotRead;
                _cache.Set("TotalMessages", messages.Liste.Count.ToString());
                _cache.Set("LastFiveMessagesNotRead", LastFiveMessagesNotRead);
                #endregion

                #region Announcements
                schoolStudent = await functions.Get<SCHOOL_STUDENT>(schoolStudent, "SchoolStudent/SchoolStudent_ListRelationalStudent?StudentID=" + needs.UserID);
                news = await functions.Get<NEWS>(news, "News/News_ListRelationalSchool?SchoolID=" + schoolStudent.Liste[0].SchoolID);
                _cache.Set("News", news.Liste);
                #endregion

                #region Last 8 Teachers
                teacherSchool = await functions.Get<TEACHER_SCHOOL>(teacherSchool, "TeacherSchool/TeacherSchool_ListRelationalSchool?SchoolID=" + schoolStudent.Liste[0].SchoolID);
                foreach (var item in teacherSchool.Liste)
                {
                    teachers = await functions.Get<TEACHER>(teachers, "Teacher/Teacher_SelectRelational?TeacherID=" + item.TeacherID);
                    teachersList.Add(teachers.Nesne);
                }
                _cache.Set("Teachers", teachersList);
                #endregion
            }





            #region Fill the ViewModel
            homeViewModel.ClassName = _cache.Get("ClassSectionName") as string;
            homeViewModel.TotalAbsence = _cache.Get("TotalAbsence") + " Gün" as string;
            homeViewModel.WeeklyLoad = _cache.Get("WeeklyLoad") + " Saat" as string;
            homeViewModel.SyllabusList = _cache.Get("Syllabus") as List<SYLLABUS>;
            homeViewModel.ExamList = _cache.Get("Exam") as List<EXAM>;
            homeViewModel.MostRecentExam = _cache.Get("MostRecentExam") as string;
            homeViewModel.ExamsLeft = _cache.Get("ExamsLeftCount") as string;
            homeViewModel.LatestGrade = _cache.Get("LatestGrade") as string;
            homeViewModel.TotalMessages = _cache.Get("TotalMessages") as string;
            homeViewModel.NewsList = _cache.Get("News") as List<NEWS>;
            homeViewModel.TeacherList = _cache.Get("Teachers") as List<TEACHER>;
            #endregion


            #region Notifications: Last Five Messages that hasn't been read and the amount of it for layout notifications
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            #endregion

            #region Sidebar FullName and Role
            ViewBag.Role = needs.LoginAs;
            ViewBag.FullName = needs.NameSurname;
            #endregion



            return View(homeViewModel);
        }


        public IActionResult RenderNotifications()
        {
            
            return PartialView("_Notifications");
        }
        public IActionResult RenderSidebar()
        {
            
            return PartialView("_Sidebar");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public class Needs
        {
            public string WebApiUrl = "";
            public string NameSurname = "";
            public string JwtToken = "";
            public string RefreshToken = "";
            public int UserID = 0;
            public string TotalNumberOfMessages = "0";
            public string LoginAs = "";
            public List<MESSAGE> LastFiveMessagesNotRead = new List<MESSAGE>();
        }

        public class Functions
        {
            public bool HasToken(string accessToken)
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            public async Task<Mesajlar<T>> Add_Update<T>(Mesajlar<T> m, string ApiURL) where T : class, new()
            {
                try
                {
                    using (HttpClientHandler handler = new HttpClientHandler())
                    {
                        using (HttpClient c = new HttpClient(handler))
                        {
                            string url = needs.WebApiUrl + ApiURL;
                            c.DefaultRequestHeaders.Add("Authorization", "Bearer " + needs.JwtToken);

                            StringContent content = new StringContent(JsonConvert.SerializeObject(m.Nesne), System.Text.Encoding.UTF8, "application/json");

                            using (var response = c.PostAsync(url, content))
                            {
                                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var sonuc =  await response.Result.Content.ReadAsStringAsync();
                                    

                                    var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc);

                                    m = msg;

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    m.Durum = false;
                    m.Mesaj = ex.Message;
                    m.Status = "danger";
                }

                return m;
            }
            public async Task<Mesajlar<T>> Get<T>(Mesajlar<T> m, string ApiURL) where T : class, new()
            {
                try
                {
                    using (HttpClientHandler handler = new HttpClientHandler())
                    {
                        using (HttpClient c = new HttpClient(handler))
                        {
                            string url = needs.WebApiUrl + ApiURL;
                            c.DefaultRequestHeaders.Add("Authorization", "Bearer " + needs.JwtToken);

                            //StringContent content = new StringContent(JsonConvert.SerializeObject(m.Mesajlar.Nesne), System.Text.Encoding.UTF8, "application/json");

                            using (var response = c.GetAsync(url))
                            {
                                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var sonuc = await response.Result.Content.ReadAsStringAsync();
                                    

                                    var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc);

                                    m = msg;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    m.Durum = false;
                    m.Mesaj = ex.Message;
                    m.Status = "danger";
                }

                return m;
            }
            public List<T> GetRelational<T>(List<T> ModelList) where T : class, new()
            {
                return ModelList;

            }

        }
    }
}
