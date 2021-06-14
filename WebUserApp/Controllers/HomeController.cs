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


        public IActionResult Welcome(int UserID)
        {
            #region Initializations
            HomeViewModel homeViewModel = new HomeViewModel();
            Mesajlar<STUDENT_CLASS> studentClass = new Mesajlar<STUDENT_CLASS>();
            Mesajlar<CLASS_SECTION> classSection = new Mesajlar<CLASS_SECTION>();
            Mesajlar<ABSENCE> absence = new Mesajlar<ABSENCE>();
            double TotalAbsence = 0;
            #endregion

            #region Calculate total absence of a student
            absence = functions.Get<ABSENCE>(absence, "Absence/Absence_List");
            foreach (var item in absence.Liste)
            {
                if (item.StudentID == UserID)
                {
                    TotalAbsence += item.TotalAbsence;
                }
            }
            #endregion

            #region Find User's Class, then it's Class Section with the ID = ClassSectionName ex: 10 - Fen - A
            studentClass = functions.Get<STUDENT_CLASS>(studentClass, "StudentClass/StudentClass_SelectStudent?StudentID=" + UserID);
            classSection = functions.Get<CLASS_SECTION>(classSection, "ClassSection/ClassSection_Select?ObjectID=" + studentClass.Nesne.ClassSectionID);
            #endregion


            #region Fill the ViewModel
            homeViewModel.ClassName = classSection.Nesne.ClassSectionName;
            homeViewModel.TotalAbsence = TotalAbsence.ToString() + " Gün";
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
