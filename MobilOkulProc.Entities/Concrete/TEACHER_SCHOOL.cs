using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class TEACHER_SCHOOL: BaseEntity
    {
        [Display(Name = "Teacher")]
        [ForeignKey("Teacher")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int TeacherID { get; set; }
        public virtual TEACHER Teacher { get; set; }


        [Display(Name = "School")]
        [ForeignKey("School")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int SchoolID { get; set; }
        public virtual SCHOOL School { get; set; }
    }
}
