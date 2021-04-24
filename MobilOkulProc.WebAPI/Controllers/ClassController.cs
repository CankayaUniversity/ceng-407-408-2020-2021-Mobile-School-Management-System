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
    public class ClassController : Controller
    {
        [HttpPost("Class_Insert")]
        public IActionResult Class_Insert([FromBody] CLASS Class)
        {
            Mesajlar<CLASS> m = new clsClass_Process().Ekle(Class);

            return Json(m);
        }


        [HttpPost("Class_Update")]
        public IActionResult Class_Update([FromBody] CLASS Class)
        {
            Mesajlar<CLASS> m = new clsClass_Process().Duzelt(Class);

            return Json(m);
        }


        [HttpGet("Class_Delete")]
        public IActionResult Class_Delete(int ClassID)
        {
            clsClass_Process sProc = new clsClass_Process();

            Mesajlar<CLASS> m = sProc.Getir(x => x.ObjectID == ClassID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Class_Select")]
        public IActionResult Class_Select(int ClassID)
        {
            clsClass_Process sProc = new clsClass_Process();

            Mesajlar<CLASS> m = sProc.Getir(x => x.ObjectID == ClassID);

            return Json(m);
        }


        [HttpGet("Class_List")]
        public IActionResult Class_List()
        {
            clsClass_Process tProc = new clsClass_Process();

            Mesajlar<CLASS> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Class_SelectRelational")]
        public IActionResult Class_SelectRelational(int ClassID)
        {
            clsClass_Process tProc = new clsClass_Process();

            Mesajlar<CLASS> m = tProc.Getir_Iliskisel(x => x.ObjectID == ClassID);

            return Json(m);
        }
    }
}
