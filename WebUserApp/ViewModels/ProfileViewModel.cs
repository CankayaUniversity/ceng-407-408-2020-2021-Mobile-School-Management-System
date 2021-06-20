using MobilOkulProc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class ProfileViewModel
    {
        public List<STUDENT_PARENT> StudentParentList { get; set; }
        public STUDENT Student { get; set; }
    }
}
