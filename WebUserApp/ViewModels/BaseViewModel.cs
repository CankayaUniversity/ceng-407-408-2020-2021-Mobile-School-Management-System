using MobilOkulProc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class BaseViewModel
    {
        public List<MESSAGE> LastFiveMessagesNotRead { get; set; }
        public string TotalNumberOfMessages { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

    }
}
