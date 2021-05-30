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
        public IActionResult SchoolStudent_Insert([FromBody] SCHOOL_STUDENT SchoolStudent)
        {
            Mesajlar<SCHOOL_STUDENT> m = new clsSchoolStudent_Process().Ekle(SchoolStudent);

            return Json(m);
        }

        [HttpPost("SchoolStudent_Update")]
        public IActionResult SchoolStudent_Update([FromBody] SCHOOL_STUDENT SchoolStudent)
        {
            Mesajlar<SCHOOL_STUDENT> m = new clsSchoolStudent_Process().Duzelt(SchoolStudent);

            return Json(m);
        }

        [HttpGet("SchoolStudent_Delete")]
        public IActionResult SchoolStudent_Delete(int SchoolStudentID)
        {
            clsSchoolStudent_Process sProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = sProc.Getir(x => x.ObjectID == SchoolStudentID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("SchoolStudent_Select")]
        public IActionResult SchoolStudent_Select(int SchoolStudentID)
        {
            clsSchoolStudent_Process sProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = sProc.Getir(x => x.ObjectID == SchoolStudentID);

            return Json(m);
        }

        [HttpGet("SchoolStudent_List")]
        public IActionResult SchoolStudent_List()
        {
            clsSchoolStudent_Process tProc = new clsSchoolStudent_Process();

            Mesajlar<SCHOOL_STUDENT> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }


    }
}
