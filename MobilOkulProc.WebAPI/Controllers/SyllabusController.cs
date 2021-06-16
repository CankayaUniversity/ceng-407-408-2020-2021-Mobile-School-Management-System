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
        public async Task<IActionResult> Syllabus_Insert([FromBody] SYLLABUS Syllabus)
        {
            Mesajlar<SYLLABUS> m = await new clsSyllabus_Process().Ekle(Syllabus);

            return Json(m);
        }


        [HttpPost("Syllabus_Update")]
        public async Task<IActionResult> User_Update([FromBody] SYLLABUS Syllabus)
        {
            Mesajlar<SYLLABUS> m = await new clsSyllabus_Process().Duzelt(Syllabus);

            return Json(m);
        }

        [HttpGet("Syllabus_Delete")]
        public async Task<IActionResult> Syllabus_Delete(int SyllabusID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Getir(x => x.Status == true && x.ObjectID == SyllabusID);

            if (m.Nesne != null)
            {
                m = await tProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Syllabus_Select")]
        public async Task<IActionResult> Syllabus_Select(int SyllabusID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Getir(x => x.Status == true && x.ObjectID == SyllabusID);

            return Json(m);
        }


        [HttpGet("Syllabus_List")]
        public async Task<IActionResult> Syllabus_List()
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Syllabus_ListObject")]
        public async Task<IActionResult> Syllabus_ListObject(int ObjectID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Listele(x => x.Status == true && x.ObjectID == ObjectID);

            return Json(m);
        }

        [HttpGet("Syllabus_ListDays")]
        public async Task<IActionResult> Syllabus_ListStudent(int DaysID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Listele(x => x.Status == true && x.DaysID == DaysID);

            return Json(m);
        }
        [HttpGet("Syllabus_SelectRelational")]
        public async Task<IActionResult> Syllabus_SelectRelational(int SyllabusID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Getir_Iliskisel(x => x.Status == true && x.ObjectID == SyllabusID);

            return Json(m);
        }
        [HttpGet("Syllabus_ListRelational")]
        public async Task<IActionResult> Syllabus_ListRelational()
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Getir_ListeIliskisel(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Syllabus_ListRelationalDays")]
        public async Task<IActionResult> Syllabus_ListRelationalDays(int DaysID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.DaysID == DaysID);

            return Json(m);
        }
        [HttpGet("Syllabus_ListRelationalClassSections")]
        public async Task<IActionResult> Syllabus_ListRelationalClassSections(int ClassSectionsID)
        {
            clsSyllabus_Process tProc = new clsSyllabus_Process();

            Mesajlar<SYLLABUS> m = await tProc.Getir_ListeIliskisel(x => x.Status == true && x.ClassSectionsID == ClassSectionsID);

            return Json(m);
        }

    }
}
