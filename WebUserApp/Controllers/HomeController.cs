using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobilOkulProc.Entities.General;
using Newtonsoft.Json;
using WebUserApp.Models;

namespace WebUserApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static Needs needs = new Needs();
        public static Functions functions = new Functions();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Welcome()
        {
            if (functions.HasToken(""))
            {

            }
            return View();
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
