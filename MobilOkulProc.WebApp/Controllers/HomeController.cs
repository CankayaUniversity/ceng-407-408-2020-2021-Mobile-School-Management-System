using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MobilOkulProc.WebApp.Extensions;
using MobilOkulProc.WebApp.ViewModels;

namespace MobilOkulProc.WebApp.Controllers
{
    public class HomeController : Controller
    {
        string WebApiUrl = "";
        private  static string  NameSurname = "";

        public HomeController(IConfiguration cfg)
        {
            WebApiUrl = cfg.GetValue<string>("WebApiUrl");
        }


        public IActionResult Welcome(USER user)
        {

            MobilViewModel<USER_INFO> m = new MobilViewModel<USER_INFO>();
            if (user.NameSurname != null)
            {
                NameSurname = user.NameSurname;

            }
            
            ViewBag.NameSurname = NameSurname;
            
            return View(m);
        }
        public IActionResult BranchList()
        {
            MobilViewModel<BRANCH> m = new MobilViewModel<BRANCH>();
            Mesajlar<BRANCH> mesajlar = new Mesajlar<BRANCH>();
            m.Mesajlar = mesajlar;

            m = GetResultsFromAPI<BRANCH>(m, "Branch/Branch_List");


            return View(m);
        }
        public MobilViewModel<T> PostGetResultsFromAPI<T>(MobilViewModel<T> m, string ApiURL) where T:class, new()
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + ApiURL;

                        StringContent content = new StringContent(JsonConvert.SerializeObject(m.Mesajlar.Nesne), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.PostAsync(url, content))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc.Result);

                                if (msg.Nesne != null)
                                {
                                    HttpContext.Session.SetObject("user", msg.Nesne);
                                }
                                else
                                {
                                    m.Mesajlar.Durum = false;
                                    m.Mesajlar.Mesaj = "Bir şeyer ters gitti.";
                                    m.Mesajlar.Status = "danger";
                                    return m;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m.Mesajlar.Durum = false;
                m.Mesajlar.Mesaj = ex.Message;
                m.Mesajlar.Status = "danger";
            }

            return m;
        }
        public MobilViewModel<T> GetResultsFromAPI<T>(MobilViewModel<T> m, string ApiURL) where T : class, new()
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + ApiURL;

                        //StringContent content = new StringContent(JsonConvert.SerializeObject(m.Mesajlar.Nesne), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.GetAsync(url))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc.Result);

                                if (msg.Liste != null || msg.Nesne != null)
                                {
                                    m.Mesajlar = msg;
                                }
                                else
                                {
                                    m.Mesajlar.Durum = false;
                                    m.Mesajlar.Mesaj = "Bir şeyer ters gitti.";
                                    m.Mesajlar.Status = "danger";
                                    return m;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m.Mesajlar.Durum = false;
                m.Mesajlar.Mesaj = ex.Message;
                m.Mesajlar.Status = "danger";
            }
            
            return m;
        }



    }
}
