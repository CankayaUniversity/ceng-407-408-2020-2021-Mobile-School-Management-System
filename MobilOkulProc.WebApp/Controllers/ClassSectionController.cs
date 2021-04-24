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
    public class ClassSectionController : Controller
    {
        public IActionResult List(string Search, int? page, Mesajlar<CLASS_SECTION> mb)
        {
            ClassSectionViewModel<CLASS_SECTION> m = new ClassSectionViewModel<CLASS_SECTION>();
            Mesajlar<SECTION> Section = new Mesajlar<SECTION>();
            Mesajlar<EDUCATIONAL_TERM> EdTerm = new Mesajlar<EDUCATIONAL_TERM>();
            Mesajlar<CLASS> Class = new Mesajlar<CLASS>();
            ViewBag.NameSurname = needs.NameSurname;
            m.Mesajlar = function.Get<CLASS_SECTION>(mb, "ClassSection/ClassSection_List");
            foreach (var item in m.Mesajlar.Liste)
            {
                Section = function.Get<SECTION>(Section, "Section/Section_Select?SectionID=" + item.SectionID);
                item.Section = Section.Nesne;
                EdTerm = function.Get<EDUCATIONAL_TERM>(EdTerm, "EducationalTerm/EducationalTerm_Select?EducationalTermID=" + item.EducationTermID);
                item.EducationalTerms = EdTerm.Nesne;
                Class = function.Get<CLASS>(Class, "Class/Class_Select?ClassID=" + item.ClassID);
                item.Class = Class.Nesne;
            }
            if (Search != null)
            {
                m.Mesajlar.Liste = m.Mesajlar.Liste.Where(m => m.ClassSectionName.ToLower().Contains(Search)).ToList();
            }
            m.PagedList = m.Mesajlar.Liste.ToPagedList(page ?? 1, 25);
            return View(m);
        }
        public IActionResult Add()
        {

            Mesajlar<SECTION> Section = new Mesajlar<SECTION>();
            Mesajlar<EDUCATIONAL_TERM> EdTerm = new Mesajlar<EDUCATIONAL_TERM>();
            Mesajlar<CLASS> Class = new Mesajlar<CLASS>();
            Section = function.Get<SECTION>(Section, "Section/Section_List");
            EdTerm = function.Get<EDUCATIONAL_TERM>(EdTerm, "EducationalTerm/EducationalTerm_List");
            Class = function.Get<CLASS>(Class, "Class/Class_List");
            ClassSectionViewModel<CLASS_SECTION> viewModel = new ClassSectionViewModel<CLASS_SECTION>()
            {
                SectionList = new SelectList(Section.Liste, "ObjectID", "SectionName"),
                EducationalTermList = new SelectList(EdTerm.Liste, "ObjectID", "EducationTerm"),
                ClassList = new SelectList(Class.Liste,"ObjectID","Class_Name"),
                ClassId = -1,
                SectionId = -1,
                EducationalTermId = -1,
            };

            ViewBag.NameSurname = needs.NameSurname;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ClassSectionViewModel<CLASS_SECTION> m)
        {
            m.Mesajlar.Nesne.EducationTermID = m.EducationalTermId;
            m.Mesajlar.Nesne.SectionID = m.SectionId;
            m.Mesajlar.Nesne.ClassID = m.ClassId;
            m.Mesajlar = function.Add_Update<CLASS_SECTION>(m.Mesajlar, "ClassSection/ClassSection_Insert");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "ClassSection", m.Mesajlar);
        }
        public IActionResult Delete(int id)
        {
            ClassSectionViewModel<CLASS_SECTION> m = new ClassSectionViewModel<CLASS_SECTION>();
            m.Mesajlar = function.Get<CLASS_SECTION>(m.Mesajlar, "ClassSection/ClassSection_SelectRelational?ClassSectionID=" + id);
            ViewBag.NameSurname = needs.NameSurname;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ClassSectionViewModel<CLASS_SECTION> mb)
        {
            mb.Mesajlar = function.Get<CLASS_SECTION>(mb.Mesajlar, "ClassSection/ClassSection_Delete?ClassSectionID=" + mb.Mesajlar.Nesne.ObjectID);
            ViewBag.NameSurname = needs.NameSurname;
            if (mb.Mesajlar.Mesaj == "Bilgiler silindi")
            {
                return RedirectToAction("List", "ClassSection", mb);
            }
            return View(mb);
        }
        public IActionResult Details(int id)
        {

            Mesajlar<CLASS_SECTION> m = new Mesajlar<CLASS_SECTION>();
            m = function.Get<CLASS_SECTION>(m, "ClassSection/ClassSection_Select?ClassSectionID=" + id);
            ClassSectionViewModel<CLASS_SECTION> ClassSectionViewModel = new ClassSectionViewModel<CLASS_SECTION>();

            ViewBag.NameSurname = needs.NameSurname;
            ClassSectionViewModel.Mesajlar = m;
            Mesajlar<SECTION> Section = new Mesajlar<SECTION>();
            Mesajlar<EDUCATIONAL_TERM> EdTerm = new Mesajlar<EDUCATIONAL_TERM>();
            Mesajlar<CLASS> Class = new Mesajlar<CLASS>();
            Section = function.Get<SECTION>(Section, "Section/Section_Select?SectionID=" + m.Nesne.SectionID);
            EdTerm = function.Get<EDUCATIONAL_TERM>(EdTerm, "EducationalTerm/EducationalTerm_Select?EducationalTermID=" + m.Nesne.EducationTermID);
            Class = function.Get<CLASS>(Class, "Class/Class_Select?ClassID=" + m.Nesne.ClassID);
            ClassSectionViewModel.Mesajlar.Nesne.Section = Section.Nesne;
            ClassSectionViewModel.Mesajlar.Nesne.EducationalTerms = EdTerm.Nesne;
            ClassSectionViewModel.Mesajlar.Nesne.Class = Class.Nesne;
            return View(ClassSectionViewModel);
        }
        public IActionResult Edit(int id)
        {
            Mesajlar<SECTION> Section = new Mesajlar<SECTION>();
            Mesajlar<EDUCATIONAL_TERM> EdTerm = new Mesajlar<EDUCATIONAL_TERM>();
            Mesajlar<CLASS> Class = new Mesajlar<CLASS>();
            Section = function.Get<SECTION>(Section, "Section/Section_List");
            EdTerm = function.Get<EDUCATIONAL_TERM>(EdTerm, "EducationalTerm/EducationalTerm_List");
            Class = function.Get<CLASS>(Class, "Class/Class_List");

            Mesajlar<CLASS_SECTION> clsSection = new Mesajlar<CLASS_SECTION>();
            clsSection = function.Get<CLASS_SECTION>(clsSection, "ClassSection/ClassSection_SelectRelational?ClassSectionID=" + id);
            ClassSectionViewModel<CLASS_SECTION> ClassSectionViewModel = new ClassSectionViewModel<CLASS_SECTION>()
            {
                SectionList = new SelectList(Section.Liste, "ObjectID", "SectionName"),
                EducationalTermList = new SelectList(EdTerm.Liste, "ObjectID", "EducationTerm"),
                ClassList = new SelectList(Class.Liste,"ObjectID","Class_Name"),
                SectionId = clsSection.Nesne.SectionID,
                EducationalTermId = clsSection.Nesne.EducationTermID,
                ClassId = clsSection.Nesne.ClassID,
            };

            ClassSectionViewModel.Mesajlar = clsSection;


            ViewBag.NameSurname = needs.NameSurname;
            return View(ClassSectionViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ClassSectionViewModel<CLASS_SECTION> m)
        {
            m.Mesajlar.Nesne.EducationTermID = m.EducationalTermId;
            m.Mesajlar.Nesne.SectionID = m.SectionId;
            m.Mesajlar.Nesne.ClassID = m.ClassId;
            m.Mesajlar = function.Add_Update<CLASS_SECTION>(m.Mesajlar, "ClassSection/ClassSection_Update");
            ViewBag.NameSurname = needs.NameSurname;
            return RedirectToAction("List", "ClassSection", m);
        }
    }
}
