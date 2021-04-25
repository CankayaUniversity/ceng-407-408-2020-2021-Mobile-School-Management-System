using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class USER : BaseEntity
    {
        #region Property Stuff
        [Display(Name = "NameSurname")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string NameSurname { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Display(Name = "UserType")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int UserType { get; set; } 
        #endregion

        public virtual List<TEACHER> Teacher { get; set; }
        public virtual List<STUDENT> Student { get; set; }
        public virtual List<SCHOOL_EMPLOYER> School_Employer { get; set; }
        
        public virtual List<PARENT> Parent { get; set; }
        public virtual List<NEWS> News { get; set; }
        public virtual List<MESSAGE> Sender { get; set; }
        public virtual List<MESSAGE> Receiver { get; set; }
        public virtual List<FEEDBACK> Feedback { get;set; }
    }

    public class USER_LOGIN
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class USER_INFO
    {
        public string NameSurname { get; set; }
    }
}
