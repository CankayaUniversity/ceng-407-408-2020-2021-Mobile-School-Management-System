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
        public async Task<IActionResult> TeacherSchool_Insert([FromBody] TEACHER_SCHOOL TeacherSchool)
        {
            Mesajlar<TEACHER_SCHOOL> m = await new clsTeacherSchool_Process().Ekle(TeacherSchool);

            return Json(m);
        }

        [HttpPost("TeacherSchool_Update")]
        public async Task<IActionResult> TeacherSchool_Update([FromBody] TEACHER_SCHOOL TeacherSchool)
        {
            Mesajlar<TEACHER_SCHOOL> m = await new clsTeacherSchool_Process().Duzelt(TeacherSchool);

            return Json(m);
        }

        [HttpGet("TeacherSchool_Delete")]
        public async Task<IActionResult> TeacherSchool_Delete(int TeacherSchoolID)
        {
            clsTeacherSchool_Process sProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = await sProc.Getir(x => x.ObjectID == TeacherSchoolID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("TeacherSchool_Select")]
        public async Task<IActionResult> TeacherSchool_Select(int TeacherSchoolID)
        {
            clsTeacherSchool_Process sProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = await sProc.Getir(x => x.ObjectID == TeacherSchoolID);

            return Json(m);
        }

        [HttpGet("TeacherSchool_List")]
        public async Task<IActionResult> TeacherSchool_List()
        {
            clsTeacherSchool_Process tProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("TeacherSchool_ListRelationalSchool")]
        public async Task<IActionResult> TeacherSchool_ListRelationalSchool(int SchoolID)
        {
            clsTeacherSchool_Process tProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.SchoolID == SchoolID);

            return Json(m);
        }
        [HttpGet("TeacherSchool_ListRelationalTeacher")]
        public async Task<IActionResult> TeacherSchool_ListRelationalTeacher(int TeacherID)
        {
            clsTeacherSchool_Process tProc = new clsTeacherSchool_Process();

            Mesajlar<TEACHER_SCHOOL> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.TeacherID == TeacherID);

            return Json(m);
        }


    }
}
