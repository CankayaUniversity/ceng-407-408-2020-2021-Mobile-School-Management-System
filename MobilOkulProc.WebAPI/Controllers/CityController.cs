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
    public class CityController : Controller
    {
        [HttpPost("City_Insert")]
        public async Task<IActionResult> City_Insert([FromBody] CITY City)
        {
            Mesajlar<CITY> m = await new clsCity_Process().Ekle(City);

            return Json(m);
        }

        [HttpPost("City_Update")]
        public async Task<IActionResult> City_Update([FromBody] CITY City)
        {
            Mesajlar<CITY> m = await new clsCity_Process().Duzelt(City);

            return Json(m);
        }

        [HttpGet("City_Delete")]
        public async Task<IActionResult> City_Delete(int CityID)
        {
            clsCity_Process uProc = new clsCity_Process();

            Mesajlar<CITY> m = await uProc.Getir(x => x.ObjectID == CityID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("City_Select")]
        public async Task<IActionResult> City_Select(int CityID)
        {
            clsCity_Process uProc = new clsCity_Process();

            Mesajlar<CITY> m = await uProc.Getir(x => x.ObjectID == CityID);

            return Json(m);
        }

        [HttpGet("City_List")]
        public async Task<IActionResult> City_List()
        {
            clsCity_Process uProc = new clsCity_Process();

            Mesajlar<CITY> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
