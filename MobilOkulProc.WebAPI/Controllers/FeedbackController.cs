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
    public class FeedbackController : Controller
    {
        [HttpPost("Feedback_Insert")]
        public async Task<IActionResult> User_Insert([FromBody] FEEDBACK Feedback)
        {
            Mesajlar<FEEDBACK> m = await new clsFeedback_Process().Ekle(Feedback);

            return Json(m);
        }

        [HttpPost("Feedback_Update")]
        public async Task<IActionResult> Feedback_Update([FromBody] FEEDBACK Feedback)
        {
            Mesajlar<FEEDBACK> m = await new clsFeedback_Process().Duzelt(Feedback);

            return Json(m);
        }

        [HttpGet("Feedback_Delete")]
        public async Task<IActionResult> Feedback_Delete(int FeedbackID)
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = await uProc.Getir(x => x.ObjectID == FeedbackID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Feedback_Select")]
        public async Task<IActionResult> Feedback_Select(int FeedbackID)
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = await uProc.Getir(x => x.ObjectID == FeedbackID);

            return Json(m);
        }

        [HttpGet("Feedback_List")]
        public async Task<IActionResult> Feedback_List()
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Feedback_SelectRelational")]
        public async Task<IActionResult> Feedback_SelectRelational(int FeedbackID)
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = await uProc.Getir_Iliskisel(x => x.ObjectID == FeedbackID);

            return Json(m);
        }
    }
}
