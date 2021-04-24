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
    public class TeacherViewModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList UserList { get; set; }
        public int UserId { get; set; }
        public SelectList BranchList { get; set; }
        public int BranchId { get; set; }
    }
}
