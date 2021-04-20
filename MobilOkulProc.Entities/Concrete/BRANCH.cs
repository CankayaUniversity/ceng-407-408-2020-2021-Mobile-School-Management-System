using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class BRANCH:BaseEntity
    {
        [Display(Name = "Şube Adı")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(30)]
        public string BranchName { get; set; }

        public virtual List<TEACHER> Teacher { get; set; }
    }
}
