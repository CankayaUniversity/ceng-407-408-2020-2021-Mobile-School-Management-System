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
        public IActionResult StudentParent_Insert([FromBody] STUDENT_PARENT StudentParent)
        {
            Mesajlar<STUDENT_PARENT> m = new clsStudentParent_Process().Ekle(StudentParent);

            return Json(m);
        }

        [HttpPost("StudentParent_Update")]
        public IActionResult StudentParent_Update([FromBody] STUDENT_PARENT StudentParent)
        {
            Mesajlar<STUDENT_PARENT> m = new clsStudentParent_Process().Duzelt(StudentParent);

            return Json(m);
        }

        [HttpGet("StudentParent_Delete")]
        public IActionResult StudentParent_Delete(int StudentParentID)
        {
            clsStudentParent_Process sProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = sProc.Getir(x => x.ObjectID == StudentParentID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("StudentParent_Select")]
        public IActionResult StudentParent_Select(int StudentParentID)
        {
            clsStudentParent_Process sProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = sProc.Getir(x => x.ObjectID == StudentParentID);

            return Json(m);
        }

        [HttpGet("StudentParent_List")]
        public IActionResult StudentParent_List()
        {
            clsStudentParent_Process tProc = new clsStudentParent_Process();

            Mesajlar<STUDENT_PARENT> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }


    }
}
