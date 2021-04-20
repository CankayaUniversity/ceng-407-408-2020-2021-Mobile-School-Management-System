using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class STUDENT : BaseEntity
    {
        [Display(Name = "TcNo")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(11)]
        public string TcNo { get; set; }

        [Display(Name = "Student Number")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string StdNumber { get; set; }

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string StdName { get; set; }

        [Display(Name = "Student Surname")]
        [MaxLength(20)]
        public string StdSurname { get; set; }

        [Display(Name = "Adress1")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string Adress1 { get; set; }

        [Display(Name = "Adress2")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string Adress2 { get; set; }

        [Display(Name = "Register Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Graduate Date")]
        public DateTime GraduateDate { get; set; }

        [Display(Name = "BirthPlace")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string BirthPlace { get; set; }

        [Display(Name = "BloodType")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(10)]
        public string BloodType { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime BirthDate { get; set; }



        [Display(Name = "User Name")]
        [ForeignKey("User")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int UserID { get; set; }
        public virtual USER User { get; set; }

        
        public virtual List<STUDENT_PARENT> StudentParent { get; set; }
    }


    
}
