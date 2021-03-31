using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class NEWS : BaseEntity
    {
        [Display(Name = "News Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime NewsDate { get; set; }

        [Display(Name = "Title")]
        [MaxLength(150)]
        public string Title { get; set; }

        [Display(Name = "News Content")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string NewsContent { get; set; }

        [Display(Name = "ImageUrl")]
        [MaxLength(350)]
        public string ImageUrl { get; set; }



        #region Virtual
        // Virtual Below Here //
        [Display(Name = "EDUCATIONAL_INSTITUTION")]
        [ForeignKey("Educational_Institution")]
        public int EducationID { get; set; }
        public virtual EDUCATIONAL_INSTITUTION Educational_Institution { get; set; }

        [Display(Name = "School")]
        [ForeignKey("School")]
        public int SchoolID { get; set; }
        public virtual SCHOOL School { get; set; }

        [Display(Name = "User")]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual USER User { get; set; }
        #endregion

        #region NotMapped Stuff
        // Not Mapped Stuff Below Here //
        [NotMapped]
        public List<string> ImageExtensions = new List<string> { ".png", ".PNG", ".JPG", ".jpg", ".JPEG", ".jpeg", ".gif", ".GIF" }; 
        #endregion


    }
}
