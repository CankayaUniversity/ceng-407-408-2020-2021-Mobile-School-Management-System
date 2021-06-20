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
    public class ParentController : Controller
    {
        [HttpPost("Parent_Insert")]
        public async Task<IActionResult> Parent_Insert([FromBody] PARENT Parent)
        {
            Mesajlar<PARENT> m = await new clsParent_Process().Ekle(Parent);

            return Json(m);
        }

        [HttpPost("Parent_Update")]
        public async Task<IActionResult> School_Update([FromBody] PARENT Parent)
        {
            Mesajlar<PARENT> m = await new clsParent_Process().Duzelt(Parent);

            return Json(m);
        }

        [HttpGet("Parent_Delete")]
        public async Task<IActionResult> School_Delete(int ParentID)
        {
            clsParent_Process sProc = new clsParent_Process();

            Mesajlar<PARENT> m = await sProc.Getir(x => x.ObjectID == ParentID);

            if (m.Nesne != null)
            {
                m = await sProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Parent_Select")]
        public async Task<IActionResult> Parent_Select(int ParentID)
        {
            clsParent_Process sProc = new clsParent_Process();

            Mesajlar<PARENT> m = await sProc.Getir(x => x.ObjectID == ParentID);

            return Json(m);
        }

        [HttpGet("Parent_List")]
        public async Task<IActionResult> Parent_List()
        {
            clsParent_Process tProc = new clsParent_Process();

            Mesajlar<PARENT> m = await tProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [HttpGet("Parent_SelectRelational")]
        public async Task<IActionResult> Parent_SelectRelational(int ParentID)
        {
            clsParent_Process sProc = new clsParent_Process();

            Mesajlar<PARENT> m = await sProc.Getir_Iliskisel(x => x.ObjectID == ParentID);

            return Json(m);
        }
        [HttpGet("Parent_SelectUser")]
        public async Task<IActionResult> Parent_SelectUser(int UserID)
        {
            clsParent_Process sProc = new clsParent_Process();

            Mesajlar<PARENT> m = await sProc.Getir(x => x.UserID == UserID);

            return Json(m);
        }

    }
}
