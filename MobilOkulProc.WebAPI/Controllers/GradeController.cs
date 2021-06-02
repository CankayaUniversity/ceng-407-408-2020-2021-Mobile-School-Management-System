﻿using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : Controller
    {
        [HttpPost("Grade_Insert")]
        public IActionResult Grade_Insert([FromBody] GRADE Grade)
        {
            Mesajlar<GRADE> m = new clsGrade_Process().Ekle(Grade);

            return Json(m);
        }

        [HttpPost("Grade_Update")]
        public IActionResult Grade_Update([FromBody] GRADE Grade)
        {
            Mesajlar<GRADE> m = new clsGrade_Process().Duzelt(Grade);

            return Json(m);
        }

        [HttpGet("Grade_Delete")]
        public IActionResult Grade_Delete(int GradeID)
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = uProc.Getir(x => x.ObjectID == GradeID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Grade_Select")]
        public IActionResult Grade_Select(int GradeID)
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = uProc.Getir(x => x.ObjectID == GradeID);

            return Json(m);
        }

        [HttpGet("Grade_List")]
        public IActionResult Grade_List()
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Grade_SelectRelational")]
        public IActionResult Grade_SelectRelational(int GradeID)
        {
            clsGrade_Process tProc = new clsGrade_Process();

            Mesajlar<GRADE> m = tProc.Getir_Iliskisel(x => x.ObjectID == GradeID);

            return Json(m);
        }
    }
}