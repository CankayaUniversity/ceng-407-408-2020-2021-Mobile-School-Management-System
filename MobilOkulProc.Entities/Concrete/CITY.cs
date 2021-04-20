using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class CITY :BaseEntity
    {
        [Display(Name="City")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string CityName { get; set; }
    }
}
