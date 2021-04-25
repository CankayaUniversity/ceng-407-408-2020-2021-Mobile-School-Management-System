using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class MESSAGE : BaseEntity
    {

        [Display(Name = "Priority ID")]
        public int PriorityID { get; set; }

        [Display(Name = "Message Title")]
        [MaxLength(50)]
        public string MessageTitle { get; set; }

        [Display(Name = "Message Content")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public string MessageContent { get; set; }

        [Display(Name = "Send Time")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public DateTime SendTime { get; set; }

        [Display(Name = "Read Time")]
        public DateTime ReadTime { get; set; }

        [Display(Name = "Message Type")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public bool MessageType { get; set; }

        [Display(Name = "Sender")]
        [ForeignKey("Sender")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int SenderID { get; set; }
        public virtual USER Sender { get; set; }

        [Display(Name = "Receiver")]
        [ForeignKey("Receive")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int ReceiveID { get; set; }
        public virtual USER Receive { get; set; }
    }
}
