using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebApp.Extensions;
using MobilOkulProc.WebApp.ViewModels;
using Newtonsoft.Json;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class LoginPageController : Controller
    {
        string WebApiUrl = "";
        public LoginPageController(IConfiguration cfg)
        {
            WebApiUrl = cfg.GetValue<string>("WebApiUrl");
        }
        public IActionResult LoginPage()
        {

            return View();
        }
        
        [HttpPost]
        public IActionResult LoginPage(Mesajlar<USER_LOGIN> m)
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + "User/User_Login";

                        StringContent content = new StringContent(JsonConvert.SerializeObject(m.Nesne), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.PostAsync(url, content))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<Mesajlar<USER>>(sonuc.Result);

                                if (msg.Nesne != null)
                                {
                                    HttpContext.Session.SetObject("user", msg.Nesne);
                                   
                                    return RedirectToAction("HomePage", "HomePage", new { NameSurname = msg.Nesne.NameSurname, Mesajlar = msg.Nesne });
                                }
                                else
                                {
                                    m.Durum = false;
                                    m.Mesaj = "Kullanıcı adı veya parola hatalı!";
                                    m.Status = "danger";
                                    return View(m);
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

            return View(m);
        }


    }
}


