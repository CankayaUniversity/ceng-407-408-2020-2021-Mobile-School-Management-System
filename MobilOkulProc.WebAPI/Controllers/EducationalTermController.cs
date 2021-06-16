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
    public class EducationalTermController : Controller
    {
        [HttpPost("EducationalTerm_Insert")]
        public async Task<IActionResult> User_Insert([FromBody] EDUCATIONAL_TERM EducationalTerm)
        {
            Mesajlar<EDUCATIONAL_TERM> m = await new clsEducationalTerm_Process().Ekle(EducationalTerm);

            return Json(m);
        }

        [HttpPost("EducationalTerm_Update")]
        public async Task<IActionResult> EducationalTerm_Update([FromBody] EDUCATIONAL_TERM EducationalTerm)
        {
            Mesajlar<EDUCATIONAL_TERM> m = await new clsEducationalTerm_Process().Duzelt(EducationalTerm);

            return Json(m);
        }

        [HttpGet("EducationalTerm_Delete")]
        public async Task<IActionResult> EducationalTerm_Delete(int EducationalTermID)
        {
            clsEducationalTerm_Process uProc = new clsEducationalTerm_Process();

            Mesajlar<EDUCATIONAL_TERM> m = await uProc.Getir(x => x.ObjectID == EducationalTermID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("EducationalTerm_Select")]
        public async Task<IActionResult> EducationalTerm_Select(int EducationalTermID)
        {
            clsEducationalTerm_Process uProc = new clsEducationalTerm_Process();

            Mesajlar<EDUCATIONAL_TERM> m = await uProc.Getir(x => x.ObjectID == EducationalTermID);

            return Json(m);
        }

        [HttpGet("EducationalTerm_List")]
        public async Task<IActionResult> EducationalTerm_List()
        {
            clsEducationalTerm_Process uProc = new clsEducationalTerm_Process();

            Mesajlar<EDUCATIONAL_TERM> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
