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
    public class SchoolEmployerController : Controller
    {
        [HttpPost("SchoolEmployer_Insert")]
        public IActionResult SchoolEmployer_Insert([FromBody] SCHOOL_EMPLOYER SchoolEmployer)
        {
            Mesajlar<SCHOOL_EMPLOYER> m = new clsSchoolEmployer_Process().Ekle(SchoolEmployer);

            return Json(m);
        }

        [HttpPost("SchoolEmployer_Update")]
        public IActionResult SchoolEmployer_Update([FromBody] SCHOOL_EMPLOYER SchoolEmployer)
        {
            Mesajlar<SCHOOL_EMPLOYER> m = new clsSchoolEmployer_Process().Duzelt(SchoolEmployer);

            return Json(m);
        }

        [HttpGet("SchoolEmployer_Delete")]
        public IActionResult SchoolEmployer_Delete(int SchoolEmployerID)
        {
            clsSchoolEmployer_Process sProc = new clsSchoolEmployer_Process();

            Mesajlar<SCHOOL_EMPLOYER> m = sProc.Getir(x => x.ObjectID == SchoolEmployerID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("SchoolEmployer_Select")]
        public IActionResult SchoolEmployer_Select(int SchoolEmployerID)
        {
            clsSchoolEmployer_Process sProc = new clsSchoolEmployer_Process();

            Mesajlar<SCHOOL_EMPLOYER> m = sProc.Getir(x => x.ObjectID == SchoolEmployerID);

            return Json(m);
        }

        [HttpGet("SchoolEmployer_List")]
        public IActionResult StudentClass_List()
        {
            clsSchoolEmployer_Process tProc = new clsSchoolEmployer_Process();

            Mesajlar<SCHOOL_EMPLOYER> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("SchoolEmployer_SelectRelational")]
        public IActionResult SchoolEmployer_SelectRelational(int SchoolEmployerID)
        {
            clsSchoolEmployer_Process sProc = new clsSchoolEmployer_Process();

            Mesajlar<SCHOOL_EMPLOYER> m = sProc.Getir_Iliskisel(x => x.ObjectID == SchoolEmployerID);

            return Json(m);
        }

    }
}
