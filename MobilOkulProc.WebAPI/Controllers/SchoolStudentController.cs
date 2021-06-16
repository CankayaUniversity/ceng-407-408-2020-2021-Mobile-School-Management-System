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
    public class SchoolStudentController : Controller
    {

        [HttpPost("SchoolStudent_Insert")]
        public async Task<IActionResult> SchoolStudent_Insert([FromBody] SCHOOL_STUDENT SchoolStudent)
        {
            Mesajlar<SCHOOL_STUDENT> m = await new clsSchoolStudent_Process().Ekle(SchoolStudent);

            return Json(m);
        }

        [HttpPost("SchoolStudent_Update")]
        public async Task<IActionResult> SchoolStudent_Update([FromBody] SCHOOL_STUDENT SchoolStudent)
        {
            Mesajlar<SCHOOL_STUDENT> m = await new clsSchoolStudent_Process().Duzelt(SchoolStudent);

            return Json(m);
        }

        [HttpGet("SchoolStudent_Delete")]
        public async Task<IActionResult> SchoolStudent_Delete(int SchoolStudentID)
        {
            clsSchoolStudent_Process sProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m =await sProc.Getir(x => x.ObjectID == SchoolStudentID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("SchoolStudent_Select")]
        public async Task<IActionResult> SchoolStudent_Select(int SchoolStudentID)
        {
            clsSchoolStudent_Process sProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = await sProc.Getir(x => x.ObjectID == SchoolStudentID);

            return Json(m);
        }

        [HttpGet("SchoolStudent_List")]
        public async Task<IActionResult> SchoolStudent_List()
        {
            clsSchoolStudent_Process tProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("SchoolStudent_ListRelationalStudent")]
        public async Task<IActionResult> SchoolStudent_ListRelationalStudent(int StudentID)
        {
            clsSchoolStudent_Process tProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.StudentID == StudentID);

            return Json(m);
        }
        [HttpGet("SchoolStudent_ListRelationalSchool")]
        public async Task<IActionResult> SchoolStudent_ListRelationalSchool(int SchoolID)
        {
            clsSchoolStudent_Process tProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.SchoolID == SchoolID);

            return Json(m);
        }


    }
}
