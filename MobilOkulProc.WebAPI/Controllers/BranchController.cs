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
        public IActionResult Branch_Insert([FromBody] BRANCH Branch)
        {
            Mesajlar<BRANCH> m = new clsBranch_Process().Ekle(Branch);

            return Json(m);
        }

        [HttpPost("Branch_Update")]
        public IActionResult Branch_Update([FromBody] BRANCH Branch)
        {
            Mesajlar<BRANCH> m = new clsBranch_Process().Duzelt(Branch);

            return Json(m);
        }

        [HttpGet("Branch_Delete")]
        public IActionResult Branch_Delete(int BranchID)
        {
            clsBranch_Process uProc = new clsBranch_Process();

            Mesajlar<BRANCH> m = uProc.Getir(x => x.ObjectID == BranchID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("Branch_Select")]
        public IActionResult Branch_Select(int BranchID)
        {
            clsBranch_Process uProc = new clsBranch_Process();

            Mesajlar<BRANCH> m = uProc.Getir(x => x.ObjectID == BranchID);

            return Json(m);
        }

        [HttpGet("Branch_List")]
        public IActionResult Branch_List()
        {
            clsBranch_Process uProc = new clsBranch_Process();

            Mesajlar<BRANCH> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
