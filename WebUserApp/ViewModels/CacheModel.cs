using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class CacheModel<T> where T : class, new()
    {
        Mesajlar<T> Mesajlar { get; set; }
    }
}
