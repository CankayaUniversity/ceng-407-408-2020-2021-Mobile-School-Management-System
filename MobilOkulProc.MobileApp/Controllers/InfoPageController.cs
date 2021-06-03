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
            ViewBag.Userno = int.Parse(HttpContext.Session.GetString("no"));
            ViewBag.Userid = int.Parse(HttpContext.Session.GetString("userid"));
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Phone = HttpContext.Session.GetString("phone");


            if (ViewBag.Userid == 1)
            {
                StudentPageModel<STUDENT> st = new StudentPageModel<STUDENT>();
                Mesajlar<STUDENT> stu = new Mesajlar<STUDENT>();
                st.Mesajlar = function.Get<STUDENT>(stu, "Student/Student_List");

                foreach (var item in st.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.Userno)
                    {

                        ViewBag.StudentNumber=item.StdNumber;
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
            else if (ViewBag.Userid == 2)
            {


                TeacherPageModel<TEACHER> t = new TeacherPageModel<TEACHER>();
                Mesajlar<TEACHER> te = new Mesajlar<TEACHER>();
                t.Mesajlar = function.Get<TEACHER>(te, "Teacher/Teacher_List");

                foreach (var item in t.Mesajlar.Liste)
                {
                    if (item.UserID == ViewBag.Userno)
                    {
                        ViewBag.TeacherTcNo = item.TcNo;
                        ViewBag.TeacherBranchID = item.BranchID;
                        ViewBag.TeacherAdress = item.Adress;
                    }
                }
            }
            else if (ViewBag.Userid == 3)
            {
                ParentPageModel<PARENT> p = new ParentPageModel<PARENT>();
                Mesajlar<PARENT> pr = new Mesajlar<PARENT>();
                p.Mesajlar = function.Get<PARENT>(pr, "Parent/Parent_List");

                foreach (var item in p.Mesajlar.Liste)
                {
                    if (item.Email == needs.Email)
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
