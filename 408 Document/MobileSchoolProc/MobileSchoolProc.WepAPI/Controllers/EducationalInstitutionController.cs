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
        public IActionResult EducationalInstitution_Insert([FromBody] EDUCATIONAL_INSTITUTION EducationalInstitution)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = new clsEducationalInstitution_Process().Ekle(EducationalInstitution);

            return Json(m);
        }

        [HttpPost("EducationalInstitution_Update")]
        public IActionResult EducationalInstitution_Update([FromBody] EDUCATIONAL_INSTITUTION EducationalInstitution)
        {
            Mesajlar<EDUCATIONAL_INSTITUTION> m = new clsEducationalInstitution_Process().Duzelt(EducationalInstitution);

            return Json(m);
        }

        [HttpGet("EducationalInstitution_Delete")]
        public IActionResult EducationalInstitution_Delete(int EducationalInstitutionID)
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = uProc.Getir(x => x.ObjectID == EducationalInstitutionID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("EducationalInstitution_Select")]
        public IActionResult EducationalInstitution_Select(int EducationalInstitutionID)
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = uProc.Getir(x => x.ObjectID == EducationalInstitutionID);

            return Json(m);
        }

        [HttpGet("EducationalInstitution_List")]
        public IActionResult EducationalInstitution_List()
        {
            clsEducationalInstitution_Process uProc = new clsEducationalInstitution_Process();

            Mesajlar<EDUCATIONAL_INSTITUTION> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
