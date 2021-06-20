using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class GradeViewModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public SelectList List { get; set; }
        public int SelectedId { get; set; }
        public SelectList List2 { get; set; }
        public int SelectedId2 { get; set; }
        public SelectList List3 { get; set; }
        public int SelectedId3 { get; set; }
    }
}
