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
    public class DaysController : Controller
    {
        [HttpPost("Days_Insert")]
        public IActionResult Days_Insert([FromBody] DAYS Days)
        {
            Mesajlar<DAYS> m = new clsDays_Process().Ekle(Days);

            return Json(m);
        }

        [HttpPost("Days_Update")]
        public IActionResult Days_Update([FromBody] DAYS Days)
        {
            Mesajlar<DAYS> m = new clsDays_Process().Duzelt(Days);

            return Json(m);
        }

        [HttpGet("Days_Delete")]
        public IActionResult Days_Delete(int DaysID)
        {
            clsDays_Process uProc = new clsDays_Process();

            Mesajlar<DAYS> m = uProc.Getir(x => x.ObjectID == DaysID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Days_Select")]
        public IActionResult Days_Select(int DaysID)
        {
            clsDays_Process uProc = new clsDays_Process();

            Mesajlar<DAYS> m = uProc.Getir(x => x.ObjectID == DaysID);

            return Json(m);
        }

        [HttpGet("Days_List")]
        public IActionResult Days_List()
        {
            clsDays_Process uProc = new clsDays_Process();

            Mesajlar<DAYS> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
