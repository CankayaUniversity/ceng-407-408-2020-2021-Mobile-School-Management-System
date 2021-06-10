using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;


namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeType : Controller
    {
        [HttpPost("GradeType_Insert")]
        public IActionResult GradeType_Insert([FromBody] GRADETYPE GradeType)
        {
            Mesajlar<GRADETYPE> m = new clsGradeType_Process().Ekle(GradeType);

            return Json(m);
        }


        [HttpPost("GradeType_Update")]
        public IActionResult GradeType_Update([FromBody] GRADETYPE GradeType)
        {
            Mesajlar<GRADETYPE> m = new clsGradeType_Process().Duzelt(GradeType);

            return Json(m);
        }


        [HttpGet("GradeType_Delete")]
        public IActionResult GradeType_Delete(int GradeTypeID)
        {
            clsGradeType_Process sProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = sProc.Getir(x => x.ObjectID == GradeTypeID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("GradeType_Select")]
        public IActionResult GradeType_Select(int GradeTypeID)
        {
            clsGradeType_Process sProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = sProc.Getir(x => x.ObjectID == GradeTypeID);

            return Json(m);
        }


        [HttpGet("GradeType_List")]
        public IActionResult GradeType_List()
        {
            clsGradeType_Process tProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("GradeType_SelectRelational")]
        public IActionResult GradeType_SelectRelational(int GradeTypeID)
        {
            clsGradeType_Process tProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = tProc.Getir_Iliskisel(x => x.ObjectID == GradeTypeID);

            return Json(m);
        }
    }
}
