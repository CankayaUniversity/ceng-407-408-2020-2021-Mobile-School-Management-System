using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeType : Controller
    {
        [HttpPost("GradeType_Insert")]
        public async Task<IActionResult> GradeType_Insert([FromBody] GRADETYPE GradeType)
        {
            Mesajlar<GRADETYPE> m = await new clsGradeType_Process().Ekle(GradeType);

            return Json(m);
        }


        [HttpPost("GradeType_Update")]
        public async Task<IActionResult> GradeType_Update([FromBody] GRADETYPE GradeType)
        {
            Mesajlar<GRADETYPE> m = await new clsGradeType_Process().Duzelt(GradeType);

            return Json(m);
        }


        [HttpGet("GradeType_Delete")]
        public async Task<IActionResult> GradeType_Delete(int GradeTypeID)
        {
            clsGradeType_Process sProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = await sProc.Getir(x => x.ObjectID == GradeTypeID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("GradeType_Select")]
        public async Task<IActionResult> GradeType_Select(int GradeTypeID)
        {
            clsGradeType_Process sProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = await sProc.Getir(x => x.ObjectID == GradeTypeID);

            return Json(m);
        }


        [HttpGet("GradeType_List")]
        public async Task<IActionResult> GradeType_List()
        {
            clsGradeType_Process tProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("GradeType_SelectRelational")]
        public async Task<IActionResult> GradeType_SelectRelational(int GradeTypeID)
        {
            clsGradeType_Process tProc = new clsGradeType_Process();

            Mesajlar<GRADETYPE> m = await tProc.Getir_Iliskisel(x => x.ObjectID == GradeTypeID);

            return Json(m);
        }
    }
}
