using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MobilOkulProc.MobileApp.Models
{
    public class ClassSectionPageModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList SectionList { get; set; }
        public int SectionId { get; set; }
        public SelectList EducationalTermList { get; set; }
        public int EducationalTermId { get; set; }
        public SelectList ClassList { get; set; }
        public int ClassId { get; set; }

    }
}
