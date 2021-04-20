using System;
using System.Collections.Generic;
using System.Text;

namespace MobilOkulProc.Entities.General
{
    public class Mesajlar<T> where T:class, new()
    {
        public bool Durum { get; set; }
        public string Status { get; set; }
        public string Mesaj { get; set; }
        public int KayitID { get; set; }
        public T Nesne { get; set; }
        public List<T> Liste { get; set; }
    }
}
