using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using MobilOkulProc.Entities.Abstract;
using MobilOkulProc.Entities.General;

namespace MobilOkulProc.Entities.Concrete
{
    public class USER : BaseEntity
    {
        #region Property Stuff
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        [JsonIgnore]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string Role { get; set; }
        #endregion

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
        public virtual List<TEACHER> Teacher { get; set; }
        public virtual List<STUDENT> Student { get; set; }
        public virtual List<SCHOOL_EMPLOYER> School_Employer { get; set; }
        
        public virtual List<PARENT> Parent { get; set; }
        public virtual List<NEWS> News { get; set; }
        public virtual List<MESSAGE> Sender { get; set; }
        public virtual List<MESSAGE> Receiver { get; set; }
        public virtual List<FEEDBACK> Feedback { get;set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
    

    public class USER_LOGIN
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
    public class USER_INFO
    {
        public string NameSurname { get; set; }
    }
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
