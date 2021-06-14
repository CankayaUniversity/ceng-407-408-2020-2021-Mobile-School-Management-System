using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;


namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AbsenceController : Controller
    {
        [HttpPost("Absence_Insert")]
        public IActionResult Absence_Insert([FromBody] ABSENCE Absence)
        {
            Mesajlar<ABSENCE> m = new clsAbsence_Process().Ekle(Absence);

            return Json(m);
        }


        [HttpPost("Absence_Update")]
        public IActionResult Absence_Update([FromBody] ABSENCE Absence)
        {
            Mesajlar<ABSENCE> m = new clsAbsence_Process().Duzelt(Absence);

            return Json(m);
        }


        [HttpGet("Absence_Delete")]
        public IActionResult Absence_Delete(int AbsenceID)
        {
            clsAbsence_Process sProc = new clsAbsence_Process();

            Mesajlar<ABSENCE> m = sProc.Getir(x => x.ObjectID == AbsenceID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Absence_Select")]
        public IActionResult Absence_Select(int AbsenceID)
        {
            clsAbsence_Process sProc = new clsAbsence_Process();

            Mesajlar<ABSENCE> m = sProc.Getir(x => x.ObjectID == AbsenceID);

            return Json(m);
        }

        [Authorize]
        [HttpGet("Absence_List")]
        public IActionResult Absence_List()
        {
            clsAbsence_Process tProc = new clsAbsence_Process();

            Mesajlar<ABSENCE> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [Authorize]
        [HttpGet("Absence_ListObject")]
        public IActionResult Absence_ListObject(int ObjectID)
        {
            clsAbsence_Process tProc = new clsAbsence_Process();

            Mesajlar<ABSENCE> m = tProc.Listele(x => x.Status == true && x.ObjectID == ObjectID);

            return Json(m);
        }
        [Authorize]
        [HttpGet("Absence_ListStudent")]
        public IActionResult Absence_ListStudent(int StudentID)
        {
            clsAbsence_Process tProc = new clsAbsence_Process();

            Mesajlar<ABSENCE> m = tProc.Listele(x => x.Status == true && x.StudentID == StudentID);

            return Json(m);
        }
        [HttpGet("Absence_SelectRelational")]
        public IActionResult Absence_SelectRelational(int AbsenceID)
        {
            clsAbsence_Process tProc = new clsAbsence_Process();

            Mesajlar<ABSENCE> m = tProc.Getir_Iliskisel(x => x.ObjectID == AbsenceID);

            return Json(m);
        }
    }
}
