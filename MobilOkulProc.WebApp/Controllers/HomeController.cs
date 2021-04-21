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
            if (user.NameSurname != null)
            {
                NameSurname = user.NameSurname;

            }
            ViewBag.NameSurname = NameSurname;
            return View();
        }

        public IActionResult ListBranch(string Search, int? page, Mesajlar<BRANCH> mb)
        {
            BranchListViewModel<BRANCH> m = new BranchListViewModel<BRANCH>();
            ViewBag.NameSurname = NameSurname;
            
            m.Mesajlar = Get<BRANCH>(mb, "Branch/Branch_List");
            if (Search != null)
            {
               m.Mesajlar.Liste =  m.Mesajlar.Liste.Where(m => m.BranchName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            if (mb.Mesaj != "")
            {
                m.Mesajlar = mb;
            }
            return View(m);
        } 
        public IActionResult AddBranch()
        {
            ViewBag.NameSurname = NameSurname;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBranch(Mesajlar<BRANCH> m)
        {
            m = Add_Update<BRANCH>(m, "Branch/Branch_Insert");
            ViewBag.NameSurname = NameSurname;
            return View(m);
        }
        public IActionResult DeleteBranch(int id)
        {
            Mesajlar<BRANCH> m = new Mesajlar<BRANCH>();
            m = Get<BRANCH>(m,"Branch/Branch_Select?BranchID="+id);
            ViewBag.NameSurname = NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBranch(Mesajlar<BRANCH> mb)
        {
            mb = Get<BRANCH>(mb, "Branch/Branch_Delete?BranchID="+mb.Nesne.ObjectID);
            ViewBag.NameSurname = NameSurname;
            if (mb.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("ListBranch", "Home", mb);
            }
            return View(mb);
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
        public Mesajlar<T> Delete_Select<T>(Mesajlar<T> m,string ApiURL) where T:class,new()
        {

            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + ApiURL;

                        StringContent content = new StringContent(JsonConvert.SerializeObject(m.Nesne), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.PostAsync(url, content))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc.Result);

                                if (msg.Mesaj == null)
                                {
                                    m.Durum = false;
                                    m.Mesaj = "Bir şeyer ters gitti.";
                                    m.Status = "danger";
                                }
                                else
                                {
                                    m = msg;
                                }
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
        public Mesajlar<T> Add_Update<T>(Mesajlar<T> m, string ApiURL) where T:class, new()
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + ApiURL;

                        StringContent content = new StringContent(JsonConvert.SerializeObject(m.Nesne), System.Text.Encoding.UTF8, "application/json");

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
                                    m.Durum = false;
                                    m.Mesaj = "Bir şeyer ters gitti.";
                                    m.Status = "danger";
                                    return m;
                                }
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
                        string url = WebApiUrl + ApiURL;

                        //StringContent content = new StringContent(JsonConvert.SerializeObject(m.Mesajlar.Nesne), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.GetAsync(url))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<Mesajlar<T>>(sonuc.Result);

                                if (msg.Liste != null || msg.Nesne != null || msg != null)
                                {
                                    m = msg;
                                }
                                else
                                {
                                    m.Durum = false;
                                    m.Mesaj = "Bir şeyler ters gitti.";
                                    m.Status = "danger";
                                    return m;
                                }
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



    }
}
