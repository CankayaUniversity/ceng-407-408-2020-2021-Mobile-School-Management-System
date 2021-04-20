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
        public IActionResult ClassSection_Insert([FromBody] CLASS_SECTION ClassSection)
        {
            Mesajlar<CLASS_SECTION> m = new clsClassSection_Process().Ekle(ClassSection);

            return Json(m);
        }

        [HttpPost("ClassSection_Update")]
        public IActionResult ClassSection_Update([FromBody] CLASS_SECTION ClassSection)
        {
            Mesajlar<CLASS_SECTION> m = new clsClassSection_Process().Duzelt(ClassSection);

            return Json(m);
        }

        [HttpGet("ClassSection_Delete")]
        public IActionResult ClassSection_Delete(int ClassSectionID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = sProc.Getir(x => x.ObjectID == ClassSectionID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("ClassSection_Select")]
        public IActionResult ClassSection_Select(int ClassSectionID)
        {
            clsClassSection_Process sProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = sProc.Getir(x => x.ObjectID == ClassSectionID);

            return Json(m);
        }

        [HttpGet("ClassSection_List")]
        public IActionResult ClassSection_List()
        {
            clsClassSection_Process tProc = new clsClassSection_Process();

            Mesajlar<CLASS_SECTION> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
