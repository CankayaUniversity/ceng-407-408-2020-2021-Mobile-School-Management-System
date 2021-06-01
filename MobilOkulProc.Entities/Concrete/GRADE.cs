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

    }
}
