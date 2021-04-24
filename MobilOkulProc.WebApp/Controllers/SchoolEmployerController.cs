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
    public class SchoolEmployerController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<SCHOOL_EMPLOYER> mb)
        {
            SchoolEmployerViewModel<SCHOOL_EMPLOYER> m = new SchoolEmployerViewModel<SCHOOL_EMPLOYER>();
            Mesajlar<USER> User = new Mesajlar<USER>();
            Mesajlar<SCHOOL> School = new Mesajlar<SCHOOL>();
            Mesajlar<EMPLOYEE_TYPE> EmpType = new Mesajlar<EMPLOYEE_TYPE>();
            Mesajlar<EDUCATIONAL_INSTITUTION> EdIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<SCHOOL_EMPLOYER>(mb, "SchoolEmployer/SchoolEmployer_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                User = function.Get<USER>(User, "User/User_Select?UserID=" + item.UserID);
                item.User = User.Nesne;
                School = function.Get<SCHOOL>(School, "School/School_Select?SchoolID=" + item.SchoolID);
                item.SCHOOL = School.Nesne;
                EmpType = function.Get<EMPLOYEE_TYPE>(EmpType, "EmployeeType/EmployeeType_Select?EmployeeTypeID=" + item.EmployerTypeID);
                item.EmployeeTypes = EmpType.Nesne;
                EdIns = function.Get<EDUCATIONAL_INSTITUTION>(EdIns, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + item.EducationID);
                item.Education = EdIns.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.NameSurname.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            Mesajlar<SCHOOL> s = new Mesajlar<SCHOOL>();
            s = function.Get<SCHOOL>(s, "School/School_List");
            Mesajlar<EMPLOYEE_TYPE> e = new Mesajlar<EMPLOYEE_TYPE>();
            e = function.Get<EMPLOYEE_TYPE>(e, "EmployeeType/EmployeeType_List");
            Mesajlar<EDUCATIONAL_INSTITUTION> ei = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            ei = function.Get<EDUCATIONAL_INSTITUTION>(ei, "EducationalInstitution/EducationalInstitution_List");
            SchoolEmployerViewModel<SCHOOL_EMPLOYER> viewModel = new SchoolEmployerViewModel<SCHOOL_EMPLOYER>()
            {
                UserList = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                SchoolList = new SelectList(s.Liste, "ObjectID", "SchoolName"),
                EmployeeTypeList = new SelectList(e.Liste,"ObjectID","EmployeeType"),
                EdInstitutionList = new SelectList(ei.Liste, "ObjectID", "EducationalName"),
                SchoolId = -1,
                UserId = -1,
                EdInstitutionId = -1,
                EmployeeTypeId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SchoolEmployerViewModel<SCHOOL_EMPLOYER> m)
        {
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar.Nesne.SchoolID = m.SchoolId;
            m.Mesajlar.Nesne.EmployerTypeID = m.EmployeeTypeId;
            m.Mesajlar.Nesne.EducationID = m.EdInstitutionId;
            m.Mesajlar = function.Add_Update<SCHOOL_EMPLOYER>(m.Mesajlar, "SchoolEmployer/SchoolEmployer_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "SchoolEmployer", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            SchoolEmployerViewModel<SCHOOL_EMPLOYER> m = new SchoolEmployerViewModel<SCHOOL_EMPLOYER>();
            m.Mesajlar = function.Get<SCHOOL_EMPLOYER>(m.Mesajlar, "SchoolEmployer/SchoolEmployer_SelectRelational?SchoolEmployerID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SchoolEmployerViewModel<SCHOOL_EMPLOYER> mb)
        {
            mb.Mesajlar = function.Get<SCHOOL_EMPLOYER>(mb.Mesajlar, "SchoolEmployer/SchoolEmployer_Delete?SchoolEmployerID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "SchoolEmployer", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<SCHOOL_EMPLOYER> m = new Mesajlar<SCHOOL_EMPLOYER>();
            m = function.Get<SCHOOL_EMPLOYER>(m, "SchoolEmployer/SchoolEmployer_Select?SchoolEmployerID=" + id);
            SchoolEmployerViewModel<SCHOOL_EMPLOYER> SchoolEmployerViewModel = new SchoolEmployerViewModel<SCHOOL_EMPLOYER>();

            ViewBag.NameSurname = needs.NameSurname;
            SchoolEmployerViewModel.Mesajlar = m;
            Mesajlar<USER> mesajlar = new Mesajlar<USER>();
            Mesajlar<SCHOOL> school = new Mesajlar<SCHOOL>();
            Mesajlar<EMPLOYEE_TYPE> empType = new Mesajlar<EMPLOYEE_TYPE>();
            Mesajlar<EDUCATIONAL_INSTITUTION> edIns = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            mesajlar = function.Get<USER>(mesajlar, "User/User_Select?UserID=" + m.Nesne.UserID);
            school = function.Get<SCHOOL>(school, "School/School_Select?SchoolID=" + m.Nesne.SchoolID);
            empType = function.Get<EMPLOYEE_TYPE>(empType, "EmployeeType/EmployeeType_Select?EmployeeTypeID=" + m.Nesne.EmployerTypeID);
            edIns = function.Get<EDUCATIONAL_INSTITUTION>(edIns, "EducationalInstitution/EducationalInstitution_Select?EducationalInstitutionID=" + m.Nesne.EducationID);
            SchoolEmployerViewModel.Mesajlar.Nesne.User = mesajlar.Nesne;
            SchoolEmployerViewModel.Mesajlar.Nesne.SCHOOL = school.Nesne;
            SchoolEmployerViewModel.Mesajlar.Nesne.EmployeeTypes = empType.Nesne;
            SchoolEmployerViewModel.Mesajlar.Nesne.Education = edIns.Nesne;
            return View(SchoolEmployerViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<USER> m = new Mesajlar<USER>();
            m = function.Get<USER>(m, "User/User_List");
            Mesajlar<SCHOOL> s = new Mesajlar<SCHOOL>();
            s = function.Get<SCHOOL>(s, "School/School_List");
            Mesajlar<EMPLOYEE_TYPE> e = new Mesajlar<EMPLOYEE_TYPE>();
            e = function.Get<EMPLOYEE_TYPE>(e, "EmployeeType/EmployeeType_List");
            Mesajlar<EDUCATIONAL_INSTITUTION> ei = new Mesajlar<EDUCATIONAL_INSTITUTION>();
            ei = function.Get<EDUCATIONAL_INSTITUTION>(ei, "EducationalInstitution/EducationalInstitution_List");

            Mesajlar<SCHOOL_EMPLOYER> schoolEmployer = new Mesajlar<SCHOOL_EMPLOYER>();
            schoolEmployer = function.Get<SCHOOL_EMPLOYER>(schoolEmployer, "SchoolEmployer/SchoolEmployer_SelectRelational?SchoolEmployerID=" + id);
            SchoolEmployerViewModel<SCHOOL_EMPLOYER> SchoolEmployerViewModel = new SchoolEmployerViewModel<SCHOOL_EMPLOYER>()
            {
                UserList = new SelectList(m.Liste, "ObjectID", "NameSurname"),
                SchoolList = new SelectList(s.Liste, "ObjectID", "SchoolName"),
                EmployeeTypeList = new SelectList(e.Liste, "ObjectID", "EmployeeType"),
                EdInstitutionList = new SelectList(ei.Liste, "ObjectID", "EducationalName"),
                SchoolId = schoolEmployer.Nesne.SchoolID,
                UserId = schoolEmployer.Nesne.UserID,
                EdInstitutionId = schoolEmployer.Nesne.EducationID,
                EmployeeTypeId = schoolEmployer.Nesne.EmployerTypeID,
            };

            SchoolEmployerViewModel.Mesajlar = schoolEmployer;


            ViewBag.NameSurname = needs.NameSurname;
            return View(SchoolEmployerViewModel);
        }
        [HttpPost]
        public IActionResult Edit(SchoolEmployerViewModel<SCHOOL_EMPLOYER> m)
        {
            m.Mesajlar.Nesne.UserID = m.UserId;
            m.Mesajlar.Nesne.SchoolID = m.SchoolId;
            m.Mesajlar.Nesne.EmployerTypeID = m.EmployeeTypeId;
            m.Mesajlar.Nesne.EducationID = m.EdInstitutionId;
            m.Mesajlar = function.Add_Update<SCHOOL_EMPLOYER>(m.Mesajlar, "SchoolEmployer/SchoolEmployer_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "SchoolEmployer", m);
        }
    }
}
