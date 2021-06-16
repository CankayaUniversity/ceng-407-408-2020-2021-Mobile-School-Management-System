using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilOkulProc.Entities.General;
using MobilOkulProc.Entities.Concrete;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {

        [HttpPost("School_Insert")]
        public async Task<IActionResult> School_Insert([FromBody] SCHOOL School)
        {
            Mesajlar<SCHOOL> m = await new clsSchooll_Proccess().Ekle(School);

            return Json(m);
        }


        [HttpPost("School_Update")]
        public async Task<IActionResult> School_Update([FromBody] SCHOOL School)
        {
            Mesajlar<SCHOOL> m = await new clsSchooll_Proccess().Duzelt(School);

            return Json(m);
        }


        [HttpGet("School_Delete")]
        public async Task<IActionResult> School_Delete(int SchoolID)
        {
            clsSchooll_Proccess sProc = new clsSchooll_Proccess();

            Mesajlar<SCHOOL> m = await sProc.Getir(x => x.ObjectID == SchoolID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("School_Select")]
        public async Task<IActionResult> School_Select(int SchoolID)
        {
            clsSchooll_Proccess sProc = new clsSchooll_Proccess();

            Mesajlar<SCHOOL> m = await sProc.Getir(x => x.ObjectID == SchoolID);

            return Json(m);
        }


        [HttpGet("School_List")]
        public async Task<IActionResult> School_List()
        {
            clsSchooll_Proccess tProc = new clsSchooll_Proccess();

            Mesajlar<SCHOOL> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("School_SelectRelational")]
        public async Task<IActionResult> School_SelectRelational(int SchoolID)
        {
            clsSchooll_Proccess tProc = new clsSchooll_Proccess();

            Mesajlar<SCHOOL> m = await tProc.Getir_Iliskisel(x => x.ObjectID == SchoolID);

            return Json(m);
        }

    }
}
