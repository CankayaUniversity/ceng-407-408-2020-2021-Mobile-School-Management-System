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
    public class TeacherController : Controller
    {
        [HttpPost("Teacher_Insert")]
        public IActionResult Teacher_Insert([FromBody] TEACHER Teacher)
        {
            Mesajlar<TEACHER> m = new clsTeacher_Process().Ekle(Teacher);

            return Json(m);
        }


        [HttpPost("Teacher_Update")]
        public IActionResult User_Update([FromBody] TEACHER Teacher)
        {
            Mesajlar<TEACHER> m = new clsTeacher_Process().Duzelt(Teacher);

            return Json(m);
        }

        [HttpGet("Teacher_Delete")]
        public IActionResult Teacher_Delete(int TeacherID)
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

            Mesajlar<TEACHER> m = tProc.Getir(x => x.ObjectID == TeacherID);

            if (m.Nesne != null)
            {
                m = tProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Teacher_Select")]
        public IActionResult Teacher_Select(int TeacherID)
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

            Mesajlar<TEACHER> m = tProc.Getir(x => x.ObjectID == TeacherID);

            return Json(m);
        }


        [HttpGet("Teacher_List")]
        public IActionResult Teacher_List()
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

            Mesajlar<TEACHER> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
