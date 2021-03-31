using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class SCHOOL_EMPLOYER : BaseEntity
    {
        [Display(Name = "Title")]
        [MaxLength(30)]
        public string Title { get; set; }

        [Display(Name = "NameSurname")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string NameSurname { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string Email { get; set; }

        #region virtual

        [Display(Name = "Education Name")]
        [ForeignKey("Education")]
        public int EducationID { get; set; }
        public virtual EDUCATIONAL_INSTITUTION Education { get; set; }

        [Display(Name = "EmployeeTypes")]
        [ForeignKey("EmployeeTypes")]
        public int EmployerTypeID { get; set; }
        public virtual EMPLOYEE_TYPE EmployeeTypes { get; set; }


        [Display(Name = "SCHOOL")]
        [ForeignKey("SCHOOL")]
        public int SchoolID { get; set; }
        public virtual SCHOOL SCHOOL { get; set; }

        [Display(Name = "User")]
        [ForeignKey("User")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int UserID { get; set; }
        public virtual USER User { get; set; }
        #endregion

    }
}
