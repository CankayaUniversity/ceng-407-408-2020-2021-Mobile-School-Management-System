using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class SYLLABUS : BaseEntity
    {
        [Display(Name = "Lecture Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime Date { get; set; }

        [Display(Name = "Lecturer")]
        [ForeignKey("Lecture")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int LectureID { get; set; }
        public virtual LECTURE Lecture { get; set; }

        [Display(Name = "Days")]
        [ForeignKey("Days")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int DaysID { get; set; }
        public virtual DAYS Days { get; set; }
    }
}
