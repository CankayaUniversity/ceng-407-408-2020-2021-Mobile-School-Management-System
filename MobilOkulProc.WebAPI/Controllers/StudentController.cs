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
        public IActionResult Student_Insert([FromBody] STUDENT Student)
        {
            Mesajlar<STUDENT> m = new clsStudent_Process().Ekle(Student);

            return Json(m);
        }

        [HttpPost("Student_Update")]
        public IActionResult Student_Update([FromBody] STUDENT Student)
        {
            Mesajlar<STUDENT> m = new clsStudent_Process().Duzelt(Student);

            return Json(m);
        }

        [HttpGet("Student_Delete")]
        public IActionResult Student_Delete(int StudentID)
        {
            clsStudent_Process sProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = sProc.Getir(x => x.ObjectID == StudentID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Student_Select")]
        public IActionResult Student_Select(int StudentID)
        {
            clsStudent_Process sProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = sProc.Getir(x => x.ObjectID == StudentID);

            return Json(m);
        }

        [HttpGet("Student_List")]
        public IActionResult Student_List()
        {
            clsStudent_Process uProc = new clsStudent_Process();

            Mesajlar<STUDENT> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }

    }
}
