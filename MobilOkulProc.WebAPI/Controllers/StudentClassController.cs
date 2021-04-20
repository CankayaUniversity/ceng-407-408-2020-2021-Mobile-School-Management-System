using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentClassController : Controller
    {
        [HttpPost("StudentClass_Insert")]
        public IActionResult StudentClass_Insert([FromBody] STUDENT_CLASS StudentClass)
        {
            Mesajlar<STUDENT_CLASS> m = new clsStudentClass_Process().Ekle(StudentClass);

            return Json(m);
        }

        [HttpPost("StudentClass_Update")]
        public IActionResult StudentClass_Update([FromBody] STUDENT_CLASS StudentClass)
        {
            Mesajlar<STUDENT_CLASS> m = new clsStudentClass_Process().Duzelt(StudentClass);

            return Json(m);
        }

        [HttpGet("StudentClass_Delete")]
        public IActionResult StudentClass_Delete(int StudentClassID)
        {
            clsStudentClass_Process sProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = sProc.Getir(x => x.ObjectID == StudentClassID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("StudentClass_Select")]
        public IActionResult StudentClass_Select(int StudentClassID)
        {
            clsStudentClass_Process sProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = sProc.Getir(x => x.ObjectID == StudentClassID);

            return Json(m);
        }


        [HttpGet("StudentClass_List")]
        public IActionResult StudentClass_List()
        {
            clsStudentClass_Process tProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
