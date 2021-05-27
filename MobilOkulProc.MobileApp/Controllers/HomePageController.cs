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

using X.PagedList;
using MobilOkulProc.MobileApp.Extensions;
using MobilOkulProc.MobileApp.Models;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class HomePageController : Controller
    {
        public static Functions function = new Functions();
        public static STUDENT studentt = new STUDENT();
        public static Needs needs = new Needs();


        public HomePageController(IConfiguration cfg)
        {
            needs.WebApiUrl = cfg.GetValue<string>("WebApiUrl");
        }

 

        public IActionResult HomePage(USER user)
        {
            //var Session = HttpContext.Session.GetObject<USER>("user");
            if (user.NameSurname != null)
            {
                needs.NameSurname = user.NameSurname;
                
            }
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.Userno = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");



            TeacherPageModel<TEACHER> t = new TeacherPageModel<TEACHER>();
            Mesajlar<TEACHER> te=new Mesajlar<TEACHER>();
            t.Mesajlar = function.Get<TEACHER>(te, "Teacher/Teacher_List");

            foreach (var item in t.Mesajlar.Liste)
            {
                if (item.ObjectID == ViewBag.Userno)
                {
                    ViewBag.TeacherTcNo = item.TcNo;
                    ViewBag.BranchID = item.BranchID;
                    ViewBag.Adress = item.Adress;
                }
            }      
               
            
            return View();
        }
        public class Needs
        {
            public string WebApiUrl = "";
            public string NameSurname = "";
        }

        public class Functions
        {
            public Mesajlar<T> Add_Update<T>(Mesajlar<T> m, string ApiURL) where T : class, new()
            {
                try
                {
                    using (HttpClientHandler handler = new HttpClientHandler())
                    {
                        using (HttpClient c = new HttpClient(handler))
                        {
                            string url = needs.WebApiUrl + ApiURL;

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
