using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MobilOkulProc.WebApp.ViewModels
{
    public class LectureViewModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList StudentList { get; set; }
        public int StudentId { get; set; }
        public SelectList TeacherList { get; set; }
        public int TeacherId { get; set; }
    }
}
