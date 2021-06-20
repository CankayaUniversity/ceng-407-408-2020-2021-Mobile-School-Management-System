using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : Controller
    {
        [HttpPost("Grade_Insert")]
        public async Task<IActionResult> Grade_Insert([FromBody] GRADE Grade)
        {
            Mesajlar<GRADE> m = await new clsGrade_Process().Ekle(Grade);

            return Json(m);
        }

        [HttpPost("Grade_Update")]
        public async Task<IActionResult> Grade_Update([FromBody] GRADE Grade)
        {
            Mesajlar<GRADE> m = await new clsGrade_Process().Duzelt(Grade);

            return Json(m);
        }

        [HttpGet("Grade_Delete")]
        public async Task<IActionResult> Grade_Delete(int GradeID)
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await uProc.Getir(x => x.ObjectID == GradeID && x.Status == true);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Grade_Select")]
        public async Task<IActionResult> Grade_Select(int GradeID)
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await uProc.Getir(x => x.ObjectID == GradeID && x.Status == true);

            return Json(m);
        }
        [HttpGet("Grade_SelectStudent")]
        public async Task<IActionResult> Grade_SelectStudent(int StudentID)
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await uProc.Getir(x => x.StudentID == StudentID && x.Status == true);

            return Json(m);
        }

        [HttpGet("Grade_List")]
        public async Task<IActionResult> Grade_List()
        {
            clsGrade_Process uProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Grade_SelectRelational")]
        public async Task<IActionResult> Grade_SelectRelational(int GradeID)
        {
            clsGrade_Process tProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await tProc.Getir_Iliskisel(x => x.ObjectID == GradeID && x.Status == true);

            return Json(m);
        }
        [HttpGet("Grade_ListRelationalStudent")]
        public async Task<IActionResult> Grade_ListRelationalStudent(int StudentID)
        {
            clsGrade_Process tProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await tProc.Getir_ListeIliskisel(x => x.StudentID == StudentID && x.Status == true);

            return Json(m);
        }
        [HttpGet("Grade_ListRelationalLecture")]
        public async Task<IActionResult> Grade_ListRelationalLecture(int LectureID)
        {
            clsGrade_Process tProc = new clsGrade_Process();

            Mesajlar<GRADE> m = await tProc.Getir_ListeIliskisel(x => x.LectureID == LectureID && x.Status == true);

            return Json(m);
        }
    }
}
