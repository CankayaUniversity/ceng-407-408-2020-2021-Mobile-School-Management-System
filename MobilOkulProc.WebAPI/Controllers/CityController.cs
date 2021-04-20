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
        public IActionResult City_Insert([FromBody] CITY City)
        {
            Mesajlar<CITY> m = new clsCity_Process().Ekle(City);

            return Json(m);
        }

        [HttpPost("City_Update")]
        public IActionResult City_Update([FromBody] CITY City)
        {
            Mesajlar<CITY> m = new clsCity_Process().Duzelt(City);

            return Json(m);
        }

        [HttpGet("City_Delete")]
        public IActionResult City_Delete(int CityID)
        {
            clsCity_Process uProc = new clsCity_Process();

            Mesajlar<CITY> m = uProc.Getir(x => x.ObjectID == CityID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("City_Select")]
        public IActionResult City_Select(int CityID)
        {
            clsCity_Process uProc = new clsCity_Process();

            Mesajlar<CITY> m = uProc.Getir(x => x.ObjectID == CityID);

            return Json(m);
        }

        [HttpGet("City_List")]
        public IActionResult City_List()
        {
            clsCity_Process uProc = new clsCity_Process();

            Mesajlar<CITY> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
