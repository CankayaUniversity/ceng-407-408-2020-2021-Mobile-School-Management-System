using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class ParentViewModel
    {
        public int SelectedId { get; set; }
        public SelectList StudentList { get; set; }
    }
}
