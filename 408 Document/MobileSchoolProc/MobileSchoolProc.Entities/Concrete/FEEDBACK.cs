using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class FEEDBACK : BaseEntity
    {
        [Display(Name = "Feedback Content")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(500)]
        public string FeedbackContent { get; set; }
        [Display(Name = "Feedback Type")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int FeedbackType { get; set; }
        [Display(Name = "Feedback Date")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime FeedbackDate { get; set; }

        [Display(Name = "User")]
        [ForeignKey("User")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int UserID { get; set; }
        public virtual USER User { get; set; }
    }
}
