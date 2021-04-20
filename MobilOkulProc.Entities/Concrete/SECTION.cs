using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class SECTION : BaseEntity
    {
        [Display(Name = "Section Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string SectionName { get; set; }

        public virtual List<CLASS_SECTION> ClassSection { get; set; }

        [ForeignKey("School")]
        public int SchoolID { get; set; }
        public virtual SCHOOL School { get; set; }
    }
}
