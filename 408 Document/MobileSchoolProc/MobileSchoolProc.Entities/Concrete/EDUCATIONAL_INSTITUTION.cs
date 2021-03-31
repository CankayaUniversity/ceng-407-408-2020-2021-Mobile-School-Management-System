using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class EDUCATIONAL_INSTITUTION:BaseEntity
    {
        [Display(Name = "Educational Name")]
        [MaxLength(50)]
        [Required(ErrorMessage ="Doldurulması zorunlu alandır!")]
        public string EducationalName { get; set; }

        [Display(Name = "Logo")]
        [MaxLength(100)]
        public string Logo { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string Phone { get; set; }

        [Display(Name = "Adres")]
        [MaxLength(300)]
        public string Adres { get; set; }

        [Display(Name = "City")]
        [ForeignKey("City")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int CityID { get; set; }

        public virtual CITY City { get; set; }

        [Display(Name = "WebSite")]
        [MaxLength(150)]
        public string WebSite { get; set; }

        public List<NEWS> News { get; set; }
        public List<SCHOOL_EMPLOYER> SchoolEmployers { get; set; }


    }
}
