using MobilOkulProc.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilOkulProc.Entities.Concrete
{
    public class LECTURE : BaseEntity
    {
        [Display(Name = "Lecture Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(100)]
        public string LectureName { get; set; }

        [Display(Name = "Students")]
        [ForeignKey("Students")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int StudentsID { get; set; }
        public virtual STUDENT Students { get; set; }

        [Display(Name = "Teacher")]
        [ForeignKey("Teacher")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int TeacherID { get; set; }
        public virtual TEACHER Teacher { get; set; }
        public virtual List<GRADE> Grade { get; set; }
        public virtual List<SYLLABUS> Syllabus { get; set; }
        public virtual List<EXAM> Exam { get; set; }
    }
}
