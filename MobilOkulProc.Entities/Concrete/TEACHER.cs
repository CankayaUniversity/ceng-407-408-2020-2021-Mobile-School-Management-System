using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class TEACHER : BaseEntity
    {

        #region Property Stuff
        [Display(Name = "TcNo")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(11)]
        public string TcNo { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Display(Name = "Adress")]
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
        #endregion

        #region Virtual Stuff

        [ForeignKey("Branch")]
        public int BranchID { get; set; }
        public virtual BRANCH Branch { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual USER User { get; set; }

        public virtual List<TEACHER_SCHOOL> TeacherSchool { get; set; }
        public virtual List<LECTURE> Lecture { get; set; }

        #endregion
        [NotMapped]
        public string FullName
        {
            get
            {
                return Name +" "+ Surname;
            }
        }






    }
}
