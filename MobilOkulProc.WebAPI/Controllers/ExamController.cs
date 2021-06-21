using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : Controller
    {
        [HttpPost("Exam_Insert")]
        public async Task<IActionResult> Exam_Insert([FromBody] EXAM Exam)
        {
            Mesajlar<EXAM> m = await new clsExam_Process().Ekle(Exam);

            return Json(m);
        }


        [HttpPost("Exam_Update")]
        public async Task<IActionResult> Exam_Update([FromBody] EXAM Exam)
        {
            Mesajlar<EXAM> m = await new clsExam_Process().Duzelt(Exam);

            return Json(m);
        }


        [HttpGet("Exam_Delete")]
        public async Task<IActionResult> Exam_Delete(int ExamID)
        {
            clsExam_Process sProc = new clsExam_Process();

            Mesajlar<EXAM> m = await sProc.Getir(x => x.ObjectID == ExamID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Exam_Select")]
        public async Task<IActionResult> Exam_Select(int ExamID)
        {
            clsExam_Process sProc = new clsExam_Process();

            Mesajlar<EXAM> m = await sProc.Getir(x => x.ObjectID == ExamID);

            return Json(m);
        }


        [HttpGet("Exam_List")]
        public async Task<IActionResult> Exam_List()
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Exam_ListLecture")]
        public async Task<IActionResult> Exam_ListLecture(int LectureID)
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = await tProc.Listele(x => x.Status == true && x.LectureID == LectureID);

            return Json(m);
        }
        [HttpGet("Exam_SelectRelational")]
        public async Task<IActionResult> Exam_SelectRelational(int ExamID)
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = await tProc.Getir_Iliskisel(x => x.ObjectID == ExamID);

            return Json(m);
        }
        [HttpGet("Exam_ListRelational")]
        public async Task<IActionResult> Exam_ListRelational(int ExamID)
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = await tProc.Getir_ListeIliskisel(x => x.ObjectID == ExamID);

            return Json(m);
        }
        [HttpGet("Exam_ListRelationalLecture")]
        public async Task<IActionResult> Exam_ListRelationalLecture(int LectureID)
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = await tProc.Getir_ListeIliskisel(x => x.LectureID == LectureID);

            return Json(m);
        }
        [HttpGet("Exam_ListRelationalClassSections")]
        public async Task<IActionResult> Exam_ListRelationalClassSections(int ClassSectionsID)
        {
            clsExam_Process tProc = new clsExam_Process();

            Mesajlar<EXAM> m = await tProc.Getir_ListeIliskisel(x => x.ClassSectionsID == ClassSectionsID && x.Status == true);

            return Json(m);
        }
    }
}
