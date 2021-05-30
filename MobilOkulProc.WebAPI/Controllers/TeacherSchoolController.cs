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
    public class TeacherSchoolController : Controller
    {

        [HttpPost("TeacherSchool_Insert")]
        public IActionResult TeacherSchool_Insert([FromBody] TEACHER_SCHOOL TeacherSchool)
        {
            Mesajlar<TEACHER_SCHOOL> m = new clsTeacherSchool_Process().Ekle(TeacherSchool);

            return Json(m);
        }

        [HttpPost("TeacherSchool_Update")]
        public IActionResult TeacherSchool_Update([FromBody] TEACHER_SCHOOL TeacherSchool)
        {
            Mesajlar<TEACHER_SCHOOL> m = new clsTeacherSchool_Process().Duzelt(TeacherSchool);

            return Json(m);
        }

        [HttpGet("TeacherSchool_Delete")]
        public IActionResult TeacherSchool_Delete(int TeacherSchoolID)
        {
            clsTeacherSchool_Process sProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = sProc.Getir(x => x.ObjectID == TeacherSchoolID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("TeacherSchool_Select")]
        public IActionResult TeacherSchool_Select(int TeacherSchoolID)
        {
            clsTeacherSchool_Process sProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = sProc.Getir(x => x.ObjectID == TeacherSchoolID);

            return Json(m);
        }

        [HttpGet("TeacherSchool_List")]
        public IActionResult TeacherSchool_List()
        {
            clsTeacherSchool_Process tProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }


    }
}
