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
        public async Task<IActionResult> Teacher_Insert([FromBody] TEACHER Teacher)
        {
             Mesajlar < TEACHER> m = await new clsTeacher_Process().Ekle(Teacher);

            return Json(m);
        }


        [HttpPost("Teacher_Update")]
        public async Task<IActionResult> User_Update([FromBody] TEACHER Teacher)
        {
             Mesajlar < TEACHER> m = await new clsTeacher_Process().Duzelt(Teacher);

            return Json(m);
        }

        [HttpGet("Teacher_Delete")]
        public async Task<IActionResult> Teacher_Delete(int TeacherID)
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

             Mesajlar < TEACHER> m = await tProc.Getir(x => x.ObjectID == TeacherID);

            if (m.Nesne != null)
            {
                m = await tProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Teacher_Select")]
        public async Task<IActionResult> Teacher_Select(int TeacherID)
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

             Mesajlar < TEACHER> m = await tProc.Getir(x => x.ObjectID == TeacherID);

            return Json(m);
        }


        [HttpGet("Teacher_List")]
        public async Task<IActionResult> Teacher_List()
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

             Mesajlar < TEACHER> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Teacher_SelectRelational")]
        public async Task<IActionResult> Teacher_SelectRelational(int TeacherID)
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

             Mesajlar<TEACHER> m = await tProc.Getir_Iliskisel(x => x.ObjectID == TeacherID && x.Status == true);

            return Json(m);
        }
        [HttpGet("Teacher_ListRelational")]
        public async Task<IActionResult> Teacher_ListRelational()
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

            Mesajlar<TEACHER> m = await tProc.Getir_ListeIliskisel(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Teacher_SelectRelationalUser")]
        public async Task<IActionResult> Teacher_SelectRelationalUser(int UserID)
        {
            clsTeacher_Process tProc = new clsTeacher_Process();

            Mesajlar<TEACHER> m = await tProc.Getir_Iliskisel(x => x.UserID == UserID && x.Status == true);

            return Json(m);
        }
    }
}
