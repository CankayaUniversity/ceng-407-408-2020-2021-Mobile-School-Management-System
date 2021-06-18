using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
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
            if (user.FullName != null)
            {
                needs.NameSurname = HttpContext.Session.GetString("user");
                

            }
            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Usertype = HttpContext.Session.GetString("userid");
           
            ViewBag.Phone = HttpContext.Session.GetString("phone");


            // Notification 
            Mesajlar<MESSAGE> notification = new Mesajlar<MESSAGE>();
            MessagePageModel<MESSAGE> notif = new MessagePageModel<MESSAGE>();
            notif.Mesajlar = function.Get<MESSAGE>(notification, "Messages/Message_List");


            int count = 0;
            foreach (var item in notif.Mesajlar.Liste)
            {
                if (item.SenderID == ViewBag.ObjectID || item.ReceiveID == ViewBag.ObjectID)
                {
                    count++;
                }
            }

            ViewBag.Notification = count;



            if (ViewBag.Usertype == "\"Student\"")
            {
                StudentPageModel<STUDENT> st = new StudentPageModel<STUDENT>();
                Mesajlar<STUDENT> stu = new Mesajlar<STUDENT>();
                st.Mesajlar = function.Get<STUDENT>(stu, "Student/Student_List");

                foreach (var item in st.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.ObjectID)
                    {

                        ViewBag.StudentNumber = item.StdNumber;
                        ViewBag.StudentParent = item.StudentParent;
                        ViewBag.StudentRegisterDate = item.RegisterDate;
                        ViewBag.StudentGraduateDate = item.GraduateDate;
                        ViewBag.StudentBloodType = item.BloodType;
                        ViewBag.StudentAdress1 = item.Adress1;
                        ViewBag.StudentAdress2 = item.Adress2;
                        ViewBag.StudentBirthDate = item.BirthDate;
                        ViewBag.StudentBirthPlace = item.BirthPlace;


                    }
                }
            }
            else if (ViewBag.Usertype == "\"Teacher\"")
            {


                TeacherPageModel<TEACHER> t = new TeacherPageModel<TEACHER>();
                Mesajlar<TEACHER> te = new Mesajlar<TEACHER>();
                t.Mesajlar = function.Get<TEACHER>(te, "Teacher/Teacher_List");

                foreach (var item in t.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.ObjectID)
                    {
                        ViewBag.TeacherTcNo = item.TcNo;
                        ViewBag.TeacherBranchID = item.BranchID;
                        ViewBag.TeacherAdress = item.Adress;
                    }
                }
            }
            else if (ViewBag.Usertype == "\"Parent\"")
            {
                ParentPageModel<PARENT> p = new ParentPageModel<PARENT>();
                Mesajlar<PARENT> pr = new Mesajlar<PARENT>();
                p.Mesajlar = function.Get<PARENT>(pr, "Parent/Parent_List");

                foreach (var item in p.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.ObjectID)
                    {
                        ViewBag.ParentPhone = item.Phone;
                        ViewBag.ParentAdress = item.Adress;

                    }
                }
            }


            return View();
        }
        public class Needs
        {
            public string WebApiUrl = "";
            public string NameSurname = "";
            public string Email = "";
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