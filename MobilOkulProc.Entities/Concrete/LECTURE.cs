using MobilOkulProc.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MobilOkulProc.Entities.Concrete
{
    public class LECTURE : BaseEntity
    {
        [Display(Name = "Lecture Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(100)]
        public string LectureName { get; set; }

        public virtual STUDENT Student { get; set; }
        public virtual TEACHER Teacher { get; set; }
        public virtual List<GRADE> Grade { get; set; }
        public virtual List<SYLLABUS> Syllabus { get; set; }
    }
}
