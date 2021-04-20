using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebApp.ViewModels
{
    public class MobilViewModel<T> where T:class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
    }
}
