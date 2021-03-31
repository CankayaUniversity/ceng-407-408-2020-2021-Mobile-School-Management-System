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
    public class MessagesController : Controller
    {
        [HttpPost("Messages_Insert")]
        public IActionResult Messages_Insert([FromBody] MESSAGE Messages)
        {
            Mesajlar<MESSAGE> m = new clsMessages_Process().Ekle(Messages);

            return Json(m);
        }

        [HttpPost("Message_Update")]
        public IActionResult Message_Update([FromBody] MESSAGE Messages)
        {
            Mesajlar<MESSAGE> m = new clsMessages_Process().Duzelt(Messages);

            return Json(m);
        }

        [HttpGet("Messages_Delete")]
        public IActionResult Messages_Delete(int MessageID)
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Getir(x => x.ObjectID == MessageID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Messages_Select")]
        public IActionResult Messages_Select(int MessageID)
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Getir(x => x.ObjectID == MessageID);

            return Json(m);
        }

        [HttpGet("Messages_List")]
        public IActionResult Messages_List()
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
