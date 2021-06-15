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
        [HttpPost("Message_Insert")]
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

        [HttpGet("Message_Delete")]
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

        [HttpGet("Message_Select")]
        public IActionResult Messages_Select(int MessageID)
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Getir(x => x.ObjectID == MessageID);

            return Json(m);
        }

        [HttpGet("Message_List")]
        public IActionResult Messages_List()
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Message_SelectRelational")]
        public IActionResult Messages_SelectRelational(int MessageID)
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Getir_Iliskisel(x => x.ObjectID == MessageID);

            return Json(m);
        }
        [HttpGet("Message_ListRelationalReceiver")]
        public IActionResult Message_ListRelationalReceiver(int ReceiveID)
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Getir_ListeIliskisel(x => x.Status == true && x.ReceiveID == ReceiveID);

            return Json(m);
        }
        [HttpGet("Message_ListRelationalReceiverNotRead")]
        public IActionResult Messages_ListReceiverNotRead(int ReceiveID)
        {
            clsMessages_Process uProc = new clsMessages_Process();

            Mesajlar<MESSAGE> m = uProc.Getir_ListeIliskisel(x => x.Status == true && x.ReceiveID == ReceiveID && x.ReadTime == null);

            return Json(m);
        }
    }
}
