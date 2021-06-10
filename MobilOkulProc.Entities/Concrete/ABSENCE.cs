using MobilOkulProc.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilOkulProc.Entities.Concrete
{
    public class ABSENCE : BaseEntity
    {

        [Display(Name = "Absence Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime AbsenceDate { get; set; }

        [Display(Name = "Absence Details")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string AbsenceDetails { get; set; }

        [Display(Name = "Absence Commentary")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string AbsenceCommentary { get; set; }


        [Display(Name = "Total Absence")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public double TotalAbsence { get; set; }

        [Display(Name = "Student")]
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int StudentID { get; set; }
        public virtual STUDENT Student { get; set; }
    }
}
