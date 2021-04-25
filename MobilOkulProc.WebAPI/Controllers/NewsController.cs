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
        public IActionResult News_Insert([FromBody] NEWS News)
        {
            Mesajlar<NEWS> m = new clsNews_Process().Ekle(News);

            return Json(m);
        }

        [HttpPost("News_Update")]
        public IActionResult News_Update([FromBody] NEWS News)
        {
            Mesajlar<NEWS> m = new clsNews_Process().Duzelt(News);

            return Json(m);
        }

        [HttpGet("News_Delete")]
        public IActionResult News_Delete(int NewsID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = uProc.Getir(x => x.ObjectID == NewsID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("News_Select")]
        public IActionResult News_Select(int NewsID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = uProc.Getir(x => x.ObjectID == NewsID);

            return Json(m);
        }

        [HttpGet("News_List")]
        public IActionResult News_List()
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("News_SelectRelational")]
        public IActionResult News_SelectRelational(int NewsID)
        {
            clsNews_Process uProc = new clsNews_Process();

            Mesajlar<NEWS> m = uProc.Getir_Iliskisel(x => x.ObjectID == NewsID);

            return Json(m);
        }


    }
}
