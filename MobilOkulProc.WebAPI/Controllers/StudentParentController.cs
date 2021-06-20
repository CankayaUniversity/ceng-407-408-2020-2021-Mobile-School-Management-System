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
    public class StudentParentController : Controller
    {

        [HttpPost("StudentParent_Insert")]
        public async Task<IActionResult> StudentParent_Insert([FromBody] STUDENT_PARENT StudentParent)
        {
            Mesajlar<STUDENT_PARENT> m = await new clsStudentParent_Process().Ekle(StudentParent);

            return Json(m);
        }

        [HttpPost("StudentParent_Update")]
        public async Task<IActionResult> StudentParent_Update([FromBody] STUDENT_PARENT StudentParent)
        {
            Mesajlar<STUDENT_PARENT> m = await new clsStudentParent_Process().Duzelt(StudentParent);

            return Json(m);
        }

        [HttpGet("StudentParent_Delete")]
        public async Task<IActionResult> StudentParent_Delete(int StudentParentID)
        {
            clsStudentParent_Process sProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = await sProc.Getir(x => x.ObjectID == StudentParentID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("StudentParent_Select")]
        public async Task<IActionResult> StudentParent_Select(int StudentParentID)
        {
            clsStudentParent_Process sProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = await sProc.Getir(x => x.ObjectID == StudentParentID);

            return Json(m);
        }

        [HttpGet("StudentParent_List")]
        public async Task<IActionResult> StudentParent_List()
        {
            clsStudentParent_Process tProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("StudentParent_ListRelationalStudent")]
        public async Task<IActionResult> StudentParent_ListRelationalStudent(int StudentID)
        {
            clsStudentParent_Process tProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.StudentID == StudentID);

            return Json(m);
        }
        [HttpGet("StudentParent_ListRelationalParent")]
        public async Task<IActionResult> StudentParent_ListRelationalParent(int ParentID)
        {
            clsStudentParent_Process tProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.ParentID == ParentID);

            return Json(m);
        }


    }
}
