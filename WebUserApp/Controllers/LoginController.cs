using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static WebUserApp.Controllers.HomeController;

namespace MobilOkulProc.WebUserApp.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                                needs.JwtToken = msg.JwtToken;
                                needs.RefreshToken = msg.RefreshToken;
                                needs.NameSurname = msg.FirstName + " " + msg.LastName;
                                needs.UserID = msg.Id;
                                needs.LoginAs = msg.Role;
                                //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55600/authorization");
                                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                //var request = new HttpRequestMessage();
                                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", msg.Token);
                                if (true)
                                {
                                    //HttpContext.Session.SetObject("Authorization", msg);
                                    //HttpContext.Request.Headers.Add("Bearer",msg.Token);
                                    return RedirectToAction("WelcomeStudent", "Home");
                                }
                                else
                                {
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
    }
}
