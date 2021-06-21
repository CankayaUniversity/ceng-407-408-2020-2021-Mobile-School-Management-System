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
using static MobilOkulProc.WebApp.Controllers.HomeController;

namespace MobilOkulProc.WebApp.Controllers
{
    public class LoginController : Controller
    {
        string WebApiUrl = "";
        public LoginController(IConfiguration cfg)
        {
            WebApiUrl = cfg.GetValue<string>("WebApiUrl");
        }
        [HttpGet(Name ="Login for Admin Interface")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost(Name ="Post Login for Admin Interface")]
        public IActionResult Login(AuthenticateRequest m)
        {
            try
            {
                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    using (HttpClient c = new HttpClient(handler))
                    {
                        string url = "http://localhost:63494/Users/authenticate";

                        StringContent content = new StringContent(JsonConvert.SerializeObject(m), System.Text.Encoding.UTF8, "application/json");

                        using (var response = c.PostAsync(url, content))
                        {
                            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var sonuc = response.Result.Content.ReadAsStringAsync();
                                sonuc.Wait();

                                var msg = JsonConvert.DeserializeObject<AuthenticateResponse>(sonuc.Result);
                                needs.LoginAs = msg.Role;
                                needs.JwtToken = msg.JwtToken;
                                needs.RefreshToken = msg.RefreshToken;
                                needs.NameSurname = msg.FirstName + " " + msg.LastName;
                                needs.UserID = msg.Id;



                                //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55600/authorization");
                                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                //var request = new HttpRequestMessage();
                                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", msg.Token);
                                if (needs.LoginAs == "Admin")
                                {
                                    //HttpContext.Session.SetObject("Authorization", msg);
                                    //HttpContext.Request.Headers.Add("Bearer",msg.Token);
                                    return RedirectToAction("Welcome", "Home");
                                }
                                else
                                {
                                    ViewBag.Result = "Buraya erişmek için yeterli yetkiye sahip değilsin.";
                                    ViewBag.Status = "danger";
                                    
                                    return View(m);
                                }
                            }
                        }
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                ViewBag.Result = "Kullanıcı adı veya parola hatalı!";
                ViewBag.Status = "danger";
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

                                var msg = JsonConvert.DeserializeObject<RegisterModel>(sonuc.Result);

                                if (msg != null)
                                {
                                    HttpContext.Session.SetObject("user", msg);
                                    return RedirectToAction("Login", "Home");
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

