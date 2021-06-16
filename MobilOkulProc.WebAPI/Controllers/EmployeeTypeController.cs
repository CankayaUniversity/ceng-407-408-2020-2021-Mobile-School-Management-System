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
    public class EmployeeTypeController : Controller
    {
        [HttpPost("EmployeeType_Insert")]
        public async Task<IActionResult> EmployeeType_Insert([FromBody] EMPLOYEE_TYPE EmployeeType)
        {
            Mesajlar<EMPLOYEE_TYPE> m = await new clsEmployeeType_Process().Ekle(EmployeeType);

            return Json(m);
        }

        [HttpPost("EmployeeType_Update")]
        public async Task<IActionResult> EmployeeType_Update([FromBody] EMPLOYEE_TYPE EmployeeType)
        {
            Mesajlar<EMPLOYEE_TYPE> m = await new clsEmployeeType_Process().Duzelt(EmployeeType);

            return Json(m);
        }

        [HttpGet("EmployeeType_Delete")]
        public async Task<IActionResult> EmployeeType_Delete(int EmployeeTypeID)
        {
            clsEmployeeType_Process uProc = new clsEmployeeType_Process();

            Mesajlar<EMPLOYEE_TYPE> m = await uProc.Getir(x => x.ObjectID == EmployeeTypeID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("EmployeeType_Select")]
        public async Task<IActionResult> EmployeeType_Select(int EmployeeTypeID)
        {
            clsEmployeeType_Process uProc = new clsEmployeeType_Process();

            Mesajlar<EMPLOYEE_TYPE> m = await uProc.Getir(x => x.ObjectID == EmployeeTypeID);

            return Json(m);
        }

        [HttpGet("EmployeeType_List")]
        public async Task<IActionResult> EmployeeType_List()
        {
            clsEmployeeType_Process uProc = new clsEmployeeType_Process();

            Mesajlar<EMPLOYEE_TYPE> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
    }
}
