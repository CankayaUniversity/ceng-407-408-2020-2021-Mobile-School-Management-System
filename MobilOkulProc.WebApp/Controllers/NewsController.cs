using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebApp.ViewModels;
using X.PagedList;
using MobilOkulProc.WebApp.Controllers;
using static MobilOkulProc.WebApp.Controllers.HomeController;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MobilOkulProc.WebApp.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<NEWS> mb)
        {
            NewsViewModel<NEWS> m = new NewsViewModel<NEWS>();
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<NEWS>(mb, "News/News_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                EdIns = function.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + item.EducationID);
                item.Educational_Institution = EdIns.Nesne;
                School = function.Get<SCHOOL>(School, "School/School_Select?SchoolID=" + item.SchoolID);
                item.School = School.Nesne;
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.NewsContent.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            EdIns = function.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_List");
            School = function.Get<SCHOOL>(School, "School/School_List");
            User = function.Get<USER>(User, "User/User_List");
            NewsViewModel<NEWS> viewModel = new NewsViewModel<NEWS>()
            {
                EducationalInstitutionList = new SelectList(EdIns.Liste, "ObjectID", "EducationalName"),
                SchoolList = new SelectList(School.Liste, "ObjectID", "SchoolName"),
                UserList = new SelectList(User.Liste, "ObjectID", "FullName"),
                EducationalInstitutionId = -1,
                SchoolId = -1,
                UserId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(NewsViewModel<NEWS> m)
        {
            m.Mesajlar.Nesne.EducationID = m.EducationalInstitutionId;
            m.Mesajlar.Nesne.SchoolID = m.SchoolId;
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar = function.Add_Update<NEWS>(m.Mesajlar, "News/News_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "News", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            NewsViewModel<NEWS> m = new NewsViewModel<NEWS>();
            m.Mesajlar = function.Get<NEWS>(m.Mesajlar, "News/News_SelectRelational?NewsID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(NewsViewModel<NEWS> mb)
        {
            mb.Mesajlar = function.Get<NEWS>(mb.Mesajlar, "News/News_Delete?NewsID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "News", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<NEWS> m = new Mesajlar<NEWS>();
            m = function.Get<NEWS>(m, "News/News_Select?NewsID=" + id);
            NewsViewModel<NEWS> NewsViewModel = new NewsViewModel<NEWS>();

            ViewBag.NameSurname = needs.NameSurname;
            NewsViewModel.Mesajlar = m;
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            EdIns = function.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + m.Nesne.EducationID);
            School = function.Get<SCHOOL>(School, "School/School_Select?SchoolID=" + m.Nesne.SchoolID);
            User = function.Get<USER>(User, "User/User_Select?UserID=" + m.Nesne.UserID);
            NewsViewModel.Mesajlar.Nesne.Educational_Institution = EdIns.Nesne;
            NewsViewModel.Mesajlar.Nesne.School = School.Nesne;
            NewsViewModel.Mesajlar.Nesne.User = User.Nesne;
            return View(NewsViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            EdIns = function.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_List");
            School = function.Get<SCHOOL>(School, "School/School_List");
            User = function.Get<USER>(User, "User/User_List");

            Mesajlar<NEWS> clsNews = new Mesajlar<NEWS>();
            clsNews = function.Get<NEWS>(clsNews, "News/News_SelectRelational?NewsID=" + id);
            NewsViewModel<NEWS> NewsViewModel = new NewsViewModel<NEWS>()
            {
                EducationalInstitutionList = new SelectList(EdIns.Liste, "ObjectID", "EducationalName"),
                SchoolList = new SelectList(School.Liste, "ObjectID", "SchoolName"),
                UserList = new SelectList(User.Liste, "ObjectID", "FullName"),
                EducationalInstitutionId = clsNews.Nesne.EducationID,
                SchoolId = clsNews.Nesne.SchoolID,
                UserId = clsNews.Nesne.UserID,
            };

            NewsViewModel.Mesajlar = clsNews;


            ViewBag.NameSurname = needs.NameSurname;
            return View(NewsViewModel);
        }
        [HttpPost]
        public IActionResult Edit(NewsViewModel<NEWS> m)
        {
            m.Mesajlar.Nesne.EducationID = m.EducationalInstitutionId;
            m.Mesajlar.Nesne.SchoolID = m.SchoolId;
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar = function.Add_Update<NEWS>(m.Mesajlar, "News/News_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "News", m);
        }
    }
}
