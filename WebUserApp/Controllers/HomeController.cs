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


namespace WebUserApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static Needs needs = new Needs();
        public static Functions functions = new Functions();
        public HomeController(ILogger<HomeController> logger, IConfiguration cfg)
        {
            _logger = logger;
            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
            
        }


        public IActionResult Welcome()
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
            double TotalAbsence = 0;
            int WeekyLoad = 0;
            string MostRecentExam = "";
            int ExamsLeftCount = 0;
            int counter = 0;
            string LatestGrade = "";
            #endregion

            #region Find User's Class, then it's Class Section with the ID = ClassSectionName ex: 10 - Fen - A
            studentClass = functions.Get<STUDENT_CLASS>(studentClass, "StudentClass/StudentClass_SelectStudent?StudentID=" + needs.UserID);
            classSection = functions.Get<CLASS_SECTION>(classSection, "ClassSection/ClassSection_Select?ObjectID=" + studentClass.Nesne.ClassSectionID);
            #endregion

            #region Calculate total absence of a student
            absence = functions.Get<ABSENCE>(absence, "Absence/Absence_ListStudent?StudentID=" + needs.UserID);
            foreach (var item in absence.Liste)
            {
                TotalAbsence += item.TotalAbsence;
            }
            #endregion

            #region Calculate Weekly Load of a Student
            syllabus = functions.Get<SYLLABUS>(syllabus, "Syllabus/Syllabus_ListRelationalClassSections?ClassSectionsID=" + studentClass.Nesne.ClassSectionID);
            foreach (var item in syllabus.Liste)
            {
                if (item.Nine != null && item.Nine != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                }
                if (item.Ten != null && item.Ten != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Eleven != null && item.Eleven != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Twelwe != null && item.Twelwe != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Thirtheen != null && item.Thirtheen != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Fourteen != null && item.Fourteen != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Fifteen != null && item.Fifteen != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Sixteen != null && item.Sixteen != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                } if (item.Seventeen != null && item.Seventeen != "Öğle Tatili")
                {
                    WeekyLoad += 1;
                }

            }
            #endregion

            #region Syllabus
            syllabus = functions.Get<SYLLABUS>(syllabus, "Syllabus/Syllabus_ListRelational");
            #endregion

            #region Exam List
            exam = functions.Get<EXAM>(exam, "Exam/Exam_ListRelationalLecture?LectureID=" + studentClass.Nesne.ClassSectionID);
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
            #endregion

            #region Latest Grade
            grade = functions.Get<GRADE>(grade, "Grade/Grade_ListRelationalStudent?StudentID=" + needs.UserID);
            LatestGrade = grade.Liste[grade.Liste.Count - 1].Lecture.LectureName + " - " + grade.Liste[grade.Liste.Count - 1].Grade;
            #endregion

            #region MessageCounts
            messages = functions.Get<MESSAGE>(messages, "Messages/Message_ListRelationalReceiverNotRead?ReceiveID=" + needs.UserID);
            for (int i = messages.Liste.Count-1, j = 0; j < 5; i--,j++)
            {
                if (i>=0)
                {
                    LastFiveMessagesNotRead.Add(messages.Liste[i]);
                }
            }
            needs.TotalNumberOfMessages = messages.Liste.Count.ToString();
            messages = functions.Get<MESSAGE>(messages, "Messages/Message_ListRelationalReceiver?ReceiveID=" + needs.UserID);
            needs.LastFiveMessagesNotRead = LastFiveMessagesNotRead;
            #endregion






            #region Fill the ViewModel
            homeViewModel.ClassName = classSection.Nesne.ClassSectionName;
            homeViewModel.TotalAbsence = TotalAbsence.ToString() + " Gün";
            homeViewModel.WeeklyLoad = WeekyLoad.ToString() + " Saat";
            homeViewModel.SyllabusList = syllabus.Liste;
            homeViewModel.ExamList = exam.Liste;
            homeViewModel.MostRecentExam = MostRecentExam;
            homeViewModel.ExamsLeft = ExamsLeftCount.ToString();
            homeViewModel.LatestGrade = LatestGrade;
            homeViewModel.TotalMessages = messages.Liste.Count.ToString();
            #endregion

            #region Layout
            ViewBag.FullName = needs.NameSurname;
            ViewBag.NotReadMessages = needs.TotalNumberOfMessages;
            ViewBag.LoginAs = needs.LoginAs;
            ViewBag.LastFiveMessagesNotRead = needs.LastFiveMessagesNotRead;
            #endregion



            return View(homeViewModel);
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
            public Mesajlar<T> Add_Update<T>(Mesajlar<T> m, string ApiURL) where T : class, new()
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
                                    var sonuc = response.Result.Content.ReadAsStringAsync();
                                    sonuc.Wait();

                                    var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc.Result);

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
            public Mesajlar<T> Get<T>(Mesajlar<T> m, string ApiURL) where T : class, new()
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
                                    var sonuc = response.Result.Content.ReadAsStringAsync();
                                    sonuc.Wait();

                                    var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc.Result);

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
