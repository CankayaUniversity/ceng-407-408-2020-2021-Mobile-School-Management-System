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
    public class SyllabusController : Controller
    {
        [HttpPost("Syllabus_Insert")]
        public IActionResult Syllabus_Insert([FromBody] SYLLABUS Syllabus)
        {
            Mesajlar<SYLLABUS> m = new clsSyllabus_Process().Ekle(Syllabus);

            return Json(m);
        }


        [HttpPost("Syllabus_Update")]
        public IActionResult User_Update([FromBody] SYLLABUS Syllabus)
        {
            Mesajlar<SYLLABUS> m = new clsSyllabus_Process().Duzelt(Syllabus);

            return Json(m);
        }

        [HttpGet("Syllabus_Delete")]
        public IActionResult Syllabus_Delete(int SyllabusID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Getir(x => x.ObjectID == SyllabusID);

            if (m.Nesne != null)
            {
                m = tProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Syllabus_Select")]
        public IActionResult Syllabus_Select(int SyllabusID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Getir(x => x.ObjectID == SyllabusID);

            return Json(m);
        }


        [HttpGet("Syllabus_List")]
        public IActionResult Syllabus_List()
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Syllabus_ListObject")]
        public IActionResult Syllabus_ListObject(int ObjectID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Listele(x => x.Status == true && x.ObjectID == ObjectID);

            return Json(m);
        }
        [HttpGet("Syllabus_ListLecture")]
        public IActionResult Syllabus_ListLecture(int LectureID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Listele(x => x.Status == true && x.LectureID == LectureID);

            return Json(m);
        }
        [HttpGet("Syllabus_ListDays")]
        public IActionResult Syllabus_ListDays(int DaysID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Listele(x => x.Status == true && x.DaysID == DaysID);

            return Json(m);
        }
        [HttpGet("Syllabus_SelectRelational")]
        public IActionResult Syllabus_SelectRelational(int SyllabusID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = tProc.Getir_Iliskisel(x => x.ObjectID == SyllabusID);

            return Json(m);
        }
    }
}
