using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class SCHOOL_STUDENT: BaseEntity
    {
        [Display(Name = "School")]
        [ForeignKey("School")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int SchoolID { get; set; }
        public virtual SCHOOL School { get; set; }


        [Display(Name = "Student")]
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int StudentID { get; set; }
        public virtual STUDENT Student { get; set; }

    }
}
