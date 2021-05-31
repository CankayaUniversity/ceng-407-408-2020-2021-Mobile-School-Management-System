using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class DAYS: BaseEntity
    {
        [Display(Name = "Days")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string DayName { get; set; }

        public virtual List<SYLLABUS> Syllabus { get; set; }
    }
}
