using MobilOkulProc.Entities.Abstract;
using System.ComponentModel.DataAnnotations;


namespace MobilOkulProc.Entities.Concrete
{
    public class GRADE : BaseEntity
    {
        [Display(Name = "Grade")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public double Grade { get; set; }


        public virtual LECTURE Lecture { get; set; }

    }
}
