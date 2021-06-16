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
        public async Task<IActionResult> StudentClass_Insert([FromBody] STUDENT_CLASS StudentClass)
        {
            Mesajlar<STUDENT_CLASS> m = await new clsStudentClass_Process().Ekle(StudentClass);

            return Json(m);
        }

        [HttpPost("StudentClass_Update")]
        public async Task<IActionResult> StudentClass_Update([FromBody] STUDENT_CLASS StudentClass)
        {
            Mesajlar<STUDENT_CLASS> m = await new clsStudentClass_Process().Duzelt(StudentClass);

            return Json(m);
        }

        [HttpGet("StudentClass_Delete")]
        public async Task<IActionResult> StudentClass_Delete(int StudentClassID)
        {
            clsStudentClass_Process sProc = new clsStudentClass_Process();
            Mesajlar<STUDENT_CLASS> m = await sProc.Getir(x => x.ObjectID == StudentClassID);
            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }
             return Json(m);
        }
        [HttpGet("StudentClass_Select")]
        public async Task<IActionResult> StudentClass_Select(int ObjectID)
        {
            clsStudentClass_Process sProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = await sProc.Getir(x => x.ObjectID == ObjectID);

            return Json(m);
        }
        [HttpGet("StudentClass_SelectStudent")]
        public async Task<IActionResult> StudentClass_SelectStudent(int StudentID)
        {
            clsStudentClass_Process sProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = await sProc.Getir(x => x.StudentID == StudentID);

            return Json(m);
        }
        [HttpGet("StudentClass_SelectClassSection")]
        public async Task<IActionResult> StudentClass_SelectClassSection(int ClassSectionID)
        {
            clsStudentClass_Process sProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = await sProc.Getir(x => x.ClassSectionID == ClassSectionID);

            return Json(m);
        }


        [HttpGet("StudentClass_List")]
        public async Task<IActionResult> StudentClass_List()
        {
            clsStudentClass_Process tProc = new clsStudentClass_Process();

            Mesajlar<STUDENT_CLASS> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
