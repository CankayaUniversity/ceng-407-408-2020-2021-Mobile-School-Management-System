using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;


namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : Controller
    {
        [HttpPost("Exam_Insert")]
        public IActionResult Exam_Insert([FromBody] EXAM Exam)
        {
            Mesajlar<EXAM> m = new clsExam_Process().Ekle(Exam);

            return Json(m);
        }


        [HttpPost("Exam_Update")]
        public IActionResult Exam_Update([FromBody] EXAM Exam)
        {
            Mesajlar<EXAM> m = new clsExam_Process().Duzelt(Exam);

            return Json(m);
        }


        [HttpGet("Exam_Delete")]
        public IActionResult Exam_Delete(int ExamID)
        {
            clsExam_Process sProc = new clsExam_Process();

            Mesajlar<EXAM> m = sProc.Getir(x => x.ObjectID == ExamID);

            if (m.Nesne != null)
            {
                m = sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Exam_Select")]
        public IActionResult Exam_Select(int ExamID)
        {
            clsExam_Process sProc = new clsExam_Process();

            Mesajlar<EXAM> m = sProc.Getir(x => x.ObjectID == ExamID);

            return Json(m);
        }


        [HttpGet("Exam_List")]
        public IActionResult Exam_List()
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Exam_SelectRelational")]
        public IActionResult Exam_SelectRelational(int ExamID)
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = tProc.Getir_Iliskisel(x => x.ObjectID == ExamID);

            return Json(m);
        }
    }
}
