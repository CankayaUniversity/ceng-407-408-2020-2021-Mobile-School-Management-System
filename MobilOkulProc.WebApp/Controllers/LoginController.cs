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
using MobilOkulProc.WebAPI;
using MobilOkulProc.WebAPI.Models;
using MobilOkulProc.WebApp.Extensions;
using MobilOkulProc.WebApp.ViewModels;
using Newtonsoft.Json;

namespace MobilOkulProc.WebApp.Controllers
{
    public class LoginController : Controller
    {
        string WebApiUrl = "";
        public LoginController(IConfiguration cfg)
        {
            WebApiUrl = cfg.GetValue<string>("WebApiUrl");
        }
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(Mesajlar<USER_LOGIN> m)
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + "Account/Login";

                        StringContent content = new StringContent(JsonConvert.SerializeObject(m.Nesne), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.PostAsync(url, content))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<AuthenticateResponse>(sonuc.Result);

                                if (msg != null)
                                {
                                    HttpContext.Session.SetObject("Authorization", msg);
                                    return RedirectToAction("Welcome", "Home", new { NameSurname = msg.FirstName});
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
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel register)
        {

            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = WebApiUrl + "User/register";

                        StringContent content = new StringContent(JsonConvert.SerializeObject(register), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.PostAsync(url, content))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<AuthenticationResponse>(sonuc.Result);

                                if (msg != null)
                                {
                                    HttpContext.Session.SetObject("user", msg);
                                    return RedirectToAction("Welcome", "Home", new { NameSurname = msg.FirstName });
                                }
                                else
                                {
                                    return View();
                                }
                            }
                        }
                    }
                }


            }
            catch
            {
                return View();
            }
            

            return View();
        }

        
    }
}

