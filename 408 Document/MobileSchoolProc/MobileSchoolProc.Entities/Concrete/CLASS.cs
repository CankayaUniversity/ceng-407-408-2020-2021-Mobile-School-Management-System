using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class CLASS: BaseEntity
    {
        [Display(Name = "Sınıf Adı")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string Class_Name { get; set; }

        [Display(Name = "School")]
        [ForeignKey("School")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int SchoolID { get; set; }
        public virtual SCHOOL School { get; set; }

        public virtual List<CLASS_SECTION> ClassSection { get; set; }
    }
}
