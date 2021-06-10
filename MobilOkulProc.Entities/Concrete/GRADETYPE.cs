using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class GRADETYPE : BaseEntity
    {

        [Display(Name = "Grade Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(255)]
        public string GradeName { get; set; }

        public virtual List<GRADE> Grade { get; set; }
    }
}
