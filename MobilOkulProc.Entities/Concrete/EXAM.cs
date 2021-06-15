using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class EXAM : BaseEntity
    {

        [Display(Name = "Exam Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime ExamDate { get; set; }

        [Display(Name = "Exam Details")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string ExamDetails { get; set; }


        [Display(Name = "ClassSections")]
        [ForeignKey("ClassSections")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int ClassSectionsID { get; set; }
        public virtual CLASS_SECTION ClassSections { get; set; }

        [Display(Name = "Lecture")]
        [ForeignKey("Lecture")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int LectureID { get; set; }
        public virtual LECTURE Lecture { get; set; }
    }
}
