using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MobilOkulProc.WebApp.ViewModels
{
    public class NewsViewModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList SchoolList { get; set; }
        public int SchoolId { get; set; }
        public SelectList EducationalInstitutionList { get; set; }
        public int EducationalInstitutionId { get; set; }
        public SelectList UserList { get; set; }
        public int UserId { get; set; }

    }
}
