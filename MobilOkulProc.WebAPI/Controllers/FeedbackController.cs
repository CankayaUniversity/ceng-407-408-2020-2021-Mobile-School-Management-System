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
        public IActionResult User_Insert([FromBody] FEEDBACK Feedback)
        {
            Mesajlar<FEEDBACK> m = new clsFeedback_Process().Ekle(Feedback);

            return Json(m);
        }

        [HttpPost("Feedback_Update")]
        public IActionResult Feedback_Update([FromBody] FEEDBACK Feedback)
        {
            Mesajlar<FEEDBACK> m = new clsFeedback_Process().Duzelt(Feedback);

            return Json(m);
        }

        [HttpGet("Feedback_Delete")]
        public IActionResult Feedback_Delete(int FeedbackID)
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = uProc.Getir(x => x.ObjectID == FeedbackID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Feedback_Select")]
        public IActionResult Feedback_Select(int FeedbackID)
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = uProc.Getir(x => x.ObjectID == FeedbackID);

            return Json(m);
        }

        [HttpGet("Feedback_List")]
        public IActionResult Feedback_List()
        {
            clsFeedback_Process uProc = new clsFeedback_Process();

            Mesajlar<FEEDBACK> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
