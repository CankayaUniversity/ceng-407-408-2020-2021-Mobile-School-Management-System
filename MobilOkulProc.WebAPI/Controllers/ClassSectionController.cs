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
    public class ClassSectionController : Controller
    {
        [HttpPost("ClassSection_Insert")]
        public async Task<IActionResult> ClassSection_Insert([FromBody] CLASS_SECTION ClassSection)
        {
            Mesajlar<CLASS_SECTION> m = await new clsClassSection_Process().Ekle(ClassSection);

            return Json(m);
        }

        [HttpPost("ClassSection_Update")]
        public async Task<IActionResult> ClassSection_Update([FromBody] CLASS_SECTION ClassSection)
        {
            Mesajlar<CLASS_SECTION> m = await new clsClassSection_Process().Duzelt(ClassSection);

            return Json(m);
        }

        [HttpGet("ClassSection_Delete")]
        public async Task<IActionResult> ClassSection_Delete(int ClassSectionID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = await sProc.Getir(x => x.ObjectID == ClassSectionID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("ClassSection_Select")]
        public async Task<IActionResult> ClassSection_Select(int ObjectID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = await sProc.Getir(x => x.ObjectID == ObjectID);

            return Json(m);
        }
        [HttpGet("ClassSection_SelectSection")]
        public async Task<IActionResult> ClassSection_SelectSection(int SectionID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = await sProc.Getir(x => x.SectionID == SectionID);

            return Json(m);
        }
        [HttpGet("ClassSection_SelectSectionEducationTerm")]
        public async Task<IActionResult> ClassSection_SelectEducationTerm(int EdicationTermID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = await sProc.Getir(x => x.EducationTermID == EdicationTermID);

            return Json(m);
        }

        [HttpGet("ClassSection_List")]
        public async Task<IActionResult> ClassSection_List()
        {
            clsClassSection_Process tProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("ClassSection_SelectRelational")]
        public async Task<IActionResult> ClassSection_SelectRelational(int ClassSectionID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = await sProc.Getir_Iliskisel(x => x.ObjectID == ClassSectionID);

            return Json(m);
        }
    }
}
