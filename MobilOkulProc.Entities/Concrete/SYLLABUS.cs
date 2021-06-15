using MobilOkulProc.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class SYLLABUS : BaseEntity
    {
        [Display(Name = "9 Dersi")]
        public string Nine { get; set; }
        [Display(Name = "10 Dersi")]
        public string Ten { get; set; }
        [Display(Name = "11 Dersi")]
        public string Eleven { get; set; }
        [Display(Name = "12 Dersi")]
        public string Twelwe { get; set; }
        [Display(Name = "13 Dersi")]
        public string Thirtheen { get; set; }
        [Display(Name = "14 Dersi")]
        public string Fourteen { get; set; }
        [Display(Name = "15 Dersi")]
        public string Fifteen { get; set; }
        [Display(Name = "16 Dersi")]
        public string Sixteen { get; set; }
        [Display(Name = "17 Dersi")]
        public string Seventeen { get; set; }



        [Display(Name = "Days")]
        [ForeignKey("Days")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int DaysID { get; set; }
        public virtual DAYS Days { get; set; }

        [Display(Name = "ClassSections")]
        [ForeignKey("ClassSections")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        public int ClassSectionsID { get; set; }
        public virtual CLASS_SECTION ClassSections { get; set; }






    }
}
