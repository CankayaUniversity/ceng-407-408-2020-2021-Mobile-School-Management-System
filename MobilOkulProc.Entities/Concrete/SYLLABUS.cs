using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class SYLLABUS : BaseEntity
    {
        [Display(Name = "Lecture Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime Date { get; set; }
        public virtual LECTURE Lecture { get; set; }
        public virtual DAYS Days { get; set; }
    }
}
