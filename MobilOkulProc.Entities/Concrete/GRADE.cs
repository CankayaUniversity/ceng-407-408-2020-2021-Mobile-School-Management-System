using MobilOkulProc.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilOkulProc.Entities.Concrete
{
    public class GRADE : BaseEntity
    {
        [Display(Name = "Grade")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public double Grade { get; set; }



        [Display(Name = "Lecture")]
        [ForeignKey("Lecture")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int LectureID { get; set; }
        public virtual LECTURE Lecture { get; set; }


        [Display(Name = "Grade Type")]
        [ForeignKey("GradeType")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int GradeTypeID { get; set; }
        public virtual GRADETYPE GradeType { get; set; }

        [Display(Name = "Student")]
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int StudentID { get; set; }
        public virtual STUDENT Student { get; set; }





    }
}
