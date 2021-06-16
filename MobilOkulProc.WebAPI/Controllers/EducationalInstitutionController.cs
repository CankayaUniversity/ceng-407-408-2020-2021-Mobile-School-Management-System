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
    public class EducationalInstitution : Controller
    {
        [HttpPost("EducationalInstitution_Insert")]
        public async Task<IActionResult> EducationalInstitution_Insert([FromBody] EDUCATIONAL_INSTITUTION EducationalInstitution)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = await new clsEducationalInstitution_Process().Ekle(EducationalInstitution);

            return Json(m);
        }

        [HttpPost("EducationalInstitution_Update")]
        public async Task<IActionResult> EducationalInstitution_Update([FromBody] EDUCATIONAL_INSTITUTION EducationalInstitution)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = await new clsEducationalInstitution_Process().Duzelt(EducationalInstitution);

            return Json(m);
        }

        [HttpGet("EducationalInstitution_Delete")]
        public async Task<IActionResult> EducationalInstitution_Delete(int EducationalInstitutionID)
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = await uProc.Getir(x => x.ObjectID == EducationalInstitutionID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("EducationalInstitution_Select")]
        public async Task<IActionResult> EducationalInstitution_Select(int EducationalInstitutionID)
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = await uProc.Getir(x => x.ObjectID == EducationalInstitutionID);

            return Json(m);
        }

        [HttpGet("EducationalInstitution_List")]
        public async Task<IActionResult> EducationalInstitution_List()
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("EducationalInstitution_SelectRelational")]
        public async Task<IActionResult> EducationalInstitution_SelectRelational(int EducationalInstitutionID)
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = await uProc.Getir_Iliskisel(x => x.ObjectID == EducationalInstitutionID);

            return Json(m);
        }
    }
}
