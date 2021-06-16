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
    public class NewsController : Controller
    {


        [HttpPost("News_Insert")]
        public async Task<IActionResult> News_Insert([FromBody] NEWS News)
        {
            Mesajlar<NEWS> m = await new clsNews_Process().Ekle(News);

            return Json(m);
        }

        [HttpPost("News_Update")]
        public async Task<IActionResult> News_Update([FromBody] NEWS News)
        {
            Mesajlar<NEWS> m = await new clsNews_Process().Duzelt(News);

            return Json(m);
        }

        [HttpGet("News_Delete")]
        public async Task<IActionResult> News_Delete(int NewsID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = await uProc.Getir(x => x.ObjectID == NewsID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("News_Select")]
        public async Task<IActionResult> News_Select(int NewsID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = await uProc.Getir(x => x.ObjectID == NewsID);

            return Json(m);
        }

        [HttpGet("News_List")]
        public async Task<IActionResult> News_List()
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("News_SelectRelational")]
        public async Task<IActionResult> News_SelectRelational(int NewsID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = await uProc.Getir_Iliskisel(x => x.ObjectID == NewsID);

            return Json(m);
        }
        [HttpGet("News_ListRelationalUser")]
        public async Task<IActionResult> News_ListRelationalUser(int UserID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = await uProc.Getir_ListeIliskisel(x => x.UserID == UserID && x.Status == true);

            return Json(m);
        }
        [HttpGet("News_ListRelationalSchool")]
        public async Task<IActionResult> News_ListRelationalSchool(int SchoolID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = await uProc.Getir_ListeIliskisel(x => x.SchoolID == SchoolID && x.Status == true);

            return Json(m);
        }


    }
}
