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
                                
                                needs.LoginAs = msg.Role;
                                needs.JwtToken = msg.JwtToken;
                                needs.RefreshToken = msg.RefreshToken;
                                needs.NameSurname = msg.FirstName + " " + msg.LastName;
                                needs.UserID = msg.Id;
                                if (needs.LoginAs == "Student")
                                {
                                   
                                    return RedirectToAction("Welcome", "Home");
                                }
                                else if(needs.LoginAs == "Parent")
                                {
                                    return RedirectToAction("ChooseChild", "Home");
                                }
                                else if (needs.LoginAs == "Teacher")
                                {
                                    return RedirectToAction("WelcomeTeacher", "Home");
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
