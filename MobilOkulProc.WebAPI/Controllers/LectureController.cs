using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LectureController : Controller
    {
        [HttpPost("Lecture_Insert")]
        public async Task<IActionResult> Lecture_Insert([FromBody] LECTURE Lecture)
        {
            Mesajlar<LECTURE> m = await new clsLecture_Process().Ekle(Lecture);

            return Json(m);
        }

        [HttpPost("Lecture_Update")]
        public async Task<IActionResult> Lecture_Update([FromBody] LECTURE Lecture)
        {
            Mesajlar<LECTURE> m = await new clsLecture_Process().Duzelt(Lecture);

            return Json(m);
        }

        [HttpGet("Lecture_Delete")]
        public async Task<IActionResult> Lecture_Delete(int LectureID)
        {
            clsLecture_Process uProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await uProc.Getir(x => x.ObjectID == LectureID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Lecture_Select")]
        public async Task<IActionResult> Lecture_Select(int LectureID)
        {
            clsLecture_Process uProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await uProc.Getir(x => x.ObjectID == LectureID);

            return Json(m);
        }

        [HttpGet("Lecture_List")]
        public async Task<IActionResult> Lecture_List()
        {
            clsLecture_Process uProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Lecture_ListClassSections")]
        public async Task<IActionResult> Lecture_ListClassSections(int ClassSectionsID)
        {
            clsLecture_Process uProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await uProc.Listele(x => x.Status == true && x.ClassSectionsID == ClassSectionsID);

            return Json(m);
        }
        [HttpGet("Lecture_ListTeacher")]
        public async Task<IActionResult> Lecture_ListTeacher(int TeacherID)
        {
            clsLecture_Process uProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await uProc.Listele(x => x.Status == true && x.TeacherID == TeacherID);

            return Json(m);
        }
        [HttpGet("Lecture_ListObject")]
        public async Task<IActionResult> Lecture_ListID(int ObjectID)
        {
            clsLecture_Process uProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await uProc.Listele(x => x.Status == true && x.ObjectID == ObjectID);

            return Json(m);
        }
        [HttpGet("Lecture_SelectRelational")]
        public async Task<IActionResult> Lecture_SelectRelational(int LectureID)
        {
            clsLecture_Process tProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await tProc.Getir_Iliskisel(x => x.ObjectID == LectureID);

            return Json(m);
        }
        [HttpGet("Lecture_ListRelationalLecture")]
        public async Task<IActionResult> Lecture_ListRelationalLecture(int LectureID)
        {
            clsLecture_Process tProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await tProc.Getir_ListeIliskisel(x => x.ObjectID == LectureID && x.Status == true);

            return Json(m);
        }
        [HttpGet("Lecture_ListRelational")]
        public async Task<IActionResult> Lecture_ListRelational()
        {
            clsLecture_Process tProc = new clsLecture_Process();

            Mesajlar<LECTURE> m = await tProc.Getir_ListeIliskisel(x => x.Status == true);

            return Json(m);
        }
    }
}
