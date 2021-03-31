using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class STUDENT_PARENT : BaseEntity
    {

        [Display(Name = "Parent")]
        [ForeignKey("Parent")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int ParentID { get; set; }
        public virtual PARENT Parent { get; set; }


        [Display(Name = "Student")]
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int StudentID { get; set; }
        public virtual STUDENT Student { get; set; }

    }
}
