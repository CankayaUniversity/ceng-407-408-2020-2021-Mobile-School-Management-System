using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class SCHOOL :BaseEntity
    {
        [Display(Name = "Education Name")]
        [ForeignKey("Education")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int EducationID { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string SchoolName { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(300)]
        public string Adress { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string Phone { get; set; }


        public virtual EDUCATIONAL_INSTITUTION Education { get; set; }
        public virtual List<CLASS> Class { get; set; }
        public virtual List<NEWS> News { get; set; }
        public virtual List<SCHOOL_EMPLOYER> SchoolEmployers { get; set; }
        public virtual List<SECTION> Section { get; set; }
    }
}
