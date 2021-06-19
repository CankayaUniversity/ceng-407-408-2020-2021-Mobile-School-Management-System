using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using MobilOkulProc.WebAPI.Models;
using Microsoft.Extensions.Options;


namespace MobilOkulProc.WebAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : Controller
    {



        [HttpPost("User_Insert")]
        public async Task<IActionResult> User_Insert([FromBody] USER User)
        {
            Mesajlar<USER> m = await new clsUser_Proccess().Ekle(User);

            return Json(m);
        }

        [HttpPost("User_Update")]
        public async Task<IActionResult> User_Update([FromBody] USER User)
        {
            Mesajlar<USER> m = await new clsUser_Proccess().Duzelt(User);

            return Json(m);
        }

        [HttpGet("User_Delete")]
        public async Task<IActionResult> User_Delete(int UserID)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = await uProc.Getir(x => x.ObjectID == UserID);

            if (m.Nesne != null)
            {
                m = await uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("User_Select")]
        public async Task<IActionResult> User_Select(int UserID)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = await uProc.Getir(x => x.ObjectID == UserID);

            return Json(m);
        }
        [HttpGet("User_SelectUsername")]
        public async Task<IActionResult> User_SelectUsername(string Username)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = await uProc.Getir(x => x.Status == true && x.Username == Username);

            return Json(m);
        }

        [HttpGet("User_List")]
        public async Task<IActionResult> User_List()
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = await uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("User_Login")]
        public async Task<IActionResult> User_Login(USER_LOGIN User)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = await uProc.Getir(x => x.Status == true && x.Username == User.UserName && x.Password == User.Password);

            return Json(m);
        }

        



    }
}
