using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MobilOkulProc.Entities.Abstract;

namespace MobilOkulProc.Entities.Concrete
{
    public class EMPLOYEE_TYPE : BaseEntity
    {
        [Display(Name = "Employee Type")]
        [Required(ErrorMessage = "Doldurulması zorunlu alandır!")]
        [MaxLength(35)]
        public string EmployeeType { get; set; }

    }
}
