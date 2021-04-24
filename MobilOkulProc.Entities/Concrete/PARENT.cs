using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class PARENT : BaseEntity
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Display(Name = "Adress")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string Adress { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string Email { get; set; }


        [Display(Name = "User")]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual USER User { get; set; }
        public virtual List<STUDENT_PARENT> StudentParent { get; set; }


        [NotMapped]
        public string FullName
        {
            get { return Name + " " + Surname; }
        }
    }
}
