using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class EDUCATIONAL_TERM : BaseEntity
    {
        [Display(Name = "Education Term")]
        [MaxLength(50)]
        public string EducationTerm { get; set; }

        public virtual List<CLASS_SECTION> ClassSection { get; set; }
    }
}
