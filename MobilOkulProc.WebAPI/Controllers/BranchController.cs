using Microsoft.AspNetCore.Authorization;
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
    public class BranchController : Controller
    {
        [HttpPost("Branch_Insert")]
        public async Task<IActionResult> Branch_Insert([FromBody] BRANCH Branch)
        {
            Mesajlar<BRANCH> m = await new clsBranch_Process().Ekle(Branch);

            return Json(m);
        }

        [HttpPost("Branch_Update")]
        public async Task<IActionResult> Branch_Update([FromBody] BRANCH Branch)
        {
            Mesajlar<BRANCH> m = await new clsBranch_Process().Duzelt(Branch);

            return Json(m);
        }

        [HttpGet("Branch_Delete")]
        public async Task<IActionResult> Branch_Delete(int BranchID)
        {
            clsBranch_Process uProc = new clsBranch_Process();

            Mesajlar<BRANCH> m = await uProc.Getir(x => x.ObjectID == BranchID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Branch_Select")]
        public async Task<IActionResult> Branch_Select(int BranchID)
        {
            clsBranch_Process uProc = new clsBranch_Process();

            Mesajlar<BRANCH> m = await uProc.Getir(x => x.ObjectID == BranchID);

            return Json(m);
        }

        [HttpGet("Branch_List")]
        public async Task<IActionResult> Branch_List()
        {
            clsBranch_Process uProc = new clsBranch_Process();

            Mesajlar<BRANCH> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
