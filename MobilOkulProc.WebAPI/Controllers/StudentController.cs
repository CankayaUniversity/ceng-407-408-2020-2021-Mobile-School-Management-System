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
    public class StudentController : Controller
    {
        [HttpPost("Student_Insert")]
        public async Task<IActionResult> Student_Insert([FromBody] STUDENT Student)
        {
            Mesajlar<STUDENT> m = await new clsStudent_Process().Ekle(Student);

            return Json(m);
        }

        [HttpPost("Student_Update")]
        public async Task<IActionResult> Student_Update([FromBody] STUDENT Student)
        {
            Mesajlar<STUDENT> m = await new clsStudent_Process().Duzelt(Student);

            return Json(m);
        }

        [HttpGet("Student_Delete")]
        public async Task<IActionResult> Student_Delete(int StudentID)
        {
            clsStudent_Process sProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = await sProc.Getir(x => x.ObjectID == StudentID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Student_Select")]
        public async Task<IActionResult> Student_Select(int StudentID)
        {
            clsStudent_Process sProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = await sProc.Getir(x => x.ObjectID == StudentID);

            return Json(m);
        }

        [HttpGet("Student_List")]
        public async Task<IActionResult> Student_List()
        {
            clsStudent_Process uProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Student_SelectRelational")]
        public async Task<IActionResult> Student_SelectRelational(int StudentID)
        {
            clsStudent_Process sProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = await sProc.Getir_Iliskisel(x => x.ObjectID == StudentID);

            return Json(m);
        }

    }
}
