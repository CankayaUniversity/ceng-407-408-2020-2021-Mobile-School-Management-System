using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MobilOkulProc.MobileApp.Models
{
    public class SyllabusPageModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList LectureList { get; set; }
        public int LectureId { get; set; }
        public SelectList DaysList { get; set; }
        public int DaysId { get; set; }
    }
}