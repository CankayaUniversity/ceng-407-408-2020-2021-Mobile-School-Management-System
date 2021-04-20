using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class CLASS_SECTION : BaseEntity
    {
        [Display(Name = "Sınıf Bölüm Adı")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string ClassSectionName { get; set; }


        [Display(Name = "Class")]
        [ForeignKey("Class")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int ClassID { get; set; }
        public virtual CLASS Class { get; set; }

        [Display(Name = "EducationalTerms")]
        [ForeignKey("EducationalTerms")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int EducationTermID { get; set; }
        public virtual EDUCATIONAL_TERM EducationalTerms { get; set; }

        [Display(Name = "Section")]
        [ForeignKey("Section")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int SectionID { get; set; }
        public virtual SECTION Section { get; set; }

        public virtual List<STUDENT_CLASS> StudentClass { get; set; }





    }
}
