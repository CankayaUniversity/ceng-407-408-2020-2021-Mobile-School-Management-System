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
using X.PagedList;

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

        public IActionResult ListBranch(string Search, int? page)
        {
            MobilViewModel<BRANCH> m = new MobilViewModel<BRANCH>();


            m.Mesajlar = GetResultsFromAPI<BRANCH>(m, "Branch/Branch_List");
            if (Search != null)
            {
               m.Mesajlar.Liste =  m.Mesajlar.Liste.Where(m => m.BranchName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);

            return View(m);
        } 
        public IActionResult AddBranch()
        {
            ViewBag.NameSurname = NameSurname;
            return View();
        }
        [HttpPost]
        public IActionResult AddBranch(MobilViewModel<BRANCH> m)
        {
            m.Mesajlar = PostResultsToAPI<BRANCH>(m, "Branch/Branch_Insert");
            ViewBag.NameSurname = NameSurname;
            return View(m);
        }
        public IActionResult DeleteBranch()
        {
            ViewBag.NameSurname = NameSurname;
            return View();
        }
        public IActionResult DetailsBranch()
        {
            ViewBag.NameSurname = NameSurname;
            return View();
        }
        public IActionResult EditBranch()
        {
            ViewBag.NameSurname = NameSurname;
            return View();
        }
        public Mesajlar<T> PostResultsToAPI<T>(MobilViewModel<T> m, string ApiURL) where T:class,new()
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

                                if (msg.Mesaj == null)
                                {
                                    m.Mesajlar.Durum = false;
                                    m.Mesajlar.Mesaj = "Bir şeyer ters gitti.";
                                    m.Mesajlar.Status = "danger";
                                }
                                else
                                {
                                    m.Mesajlar = msg;
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

            return m.Mesajlar;
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
        } //Generic function for GET results by POST'ing info from API
        public Mesajlar<T> GetResultsFromAPI<T>(MobilViewModel<T> m, string ApiURL) where T : class, new()
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
                                    return m.Mesajlar;
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
            
            return m.Mesajlar;
        } // Generic function for GET results from API



    }
}
