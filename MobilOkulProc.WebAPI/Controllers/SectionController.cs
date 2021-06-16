using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : Controller
    {
        [HttpPost("Section_Insert")]
        public async Task<IActionResult> Section_Insert([FromBody] SECTION Section)
        {
            Mesajlar<SECTION> m = await new clsSection_Process().Ekle(Section);

            return Json(m);
        }

        [HttpPost("Section_Update")]
        public async Task<IActionResult> Section_Update([FromBody] SECTION Section)
        {
            Mesajlar<SECTION> m = await new clsSection_Process().Duzelt(Section);

            return Json(m);
        }

        [HttpGet("Section_Delete")]
        public async Task<IActionResult> Section_Delete(int SectionID)
        {
            clsSection_Process sProc = new clsSection_Process();

            Mesajlar<SECTION> m = await sProc.Getir(x => x.ObjectID == SectionID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Section_Select")]
        public async Task<IActionResult> Section_Select(int SectionID)
        {
            clsSection_Process sProc = new clsSection_Process();

            Mesajlar<SECTION> m = await sProc.Getir(x => x.ObjectID == SectionID);

            return Json(m);
        }

        [HttpGet("Section_List")]
        public async Task<IActionResult> Section_List()
        {
            clsSection_Process tProc = new clsSection_Process();

            Mesajlar<SECTION> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Section_SelectRelational")]
        public async Task<IActionResult> Section_SelectRelational(int SectionID)
        {
            clsSection_Process sProc = new clsSection_Process();

            Mesajlar<SECTION> m = await sProc.Getir_Iliskisel(x => x.ObjectID == SectionID);

            return Json(m);
        }
    }
}
