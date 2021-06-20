using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.MobileApp.Models;
using static MobilOkulProc.MobileApp.Controllers.HomePageController;

namespace MobilOkulProc.MobileApp.Controllers
{
    public class InfoPageController : Controller
    {
        public IActionResult InfoPage()
        {

            ViewBag.NameSurname = needs.NameSurname;
            ViewBag.ObjectID = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Usertype = HttpContext.Session.GetString("userid");
            //
            ViewBag.Phone = HttpContext.Session.GetString("phone");

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
    }
}
