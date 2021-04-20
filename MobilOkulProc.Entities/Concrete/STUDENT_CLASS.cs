using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class STUDENT_CLASS : BaseEntity
    {


        [Display(Name = "ClassSection")]
        [ForeignKey("ClassSection")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int ClassSectionID { get; set; }
        public virtual CLASS_SECTION ClassSection { get; set; }


        [Display(Name = "Student")]
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int StudentID { get; set; }
        public virtual STUDENT Student { get; set; }

    }
}
