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
        public IActionResult Section_Insert([FromBody] SECTION Section)
        {
            Mesajlar<SECTION> m = new clsSection_Process().Ekle(Section);

            return Json(m);
        }

        [HttpPost("Section_Update")]
        public IActionResult Section_Update([FromBody] SECTION Section)
        {
            Mesajlar<SECTION> m = new clsSection_Process().Duzelt(Section);

            return Json(m);
        }

        [HttpGet("Section_Delete")]
        public IActionResult Section_Delete(int SectionID)
        {
            clsSection_Process sProc = new clsSection_Process();

            Mesajlar<SECTION> m = sProc.Getir(x => x.ObjectID == SectionID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Section_Select")]
        public IActionResult Section_Select(int SectionID)
        {
            clsSection_Process sProc = new clsSection_Process();

            Mesajlar<SECTION> m = sProc.Getir(x => x.ObjectID == SectionID);

            return Json(m);
        }

        [HttpGet("Section_List")]
        public IActionResult Section_List()
        {
            clsSection_Process tProc = new clsSection_Process();

            Mesajlar<SECTION> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
