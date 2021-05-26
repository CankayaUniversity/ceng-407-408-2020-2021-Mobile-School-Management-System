﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace MobilOkulProc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        private IJWTAuthenticationManager _jwtAuthenticationManager;
        public UserController(IJWTAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost("User_Insert")]
        public IActionResult User_Insert([FromBody] USER User)
        {
            Mesajlar<USER> m = new clsUser_Proccess().Ekle(User);

            return Json(m);
        }

        [HttpPost("User_Update")]
        public IActionResult User_Update([FromBody] USER User)
        {
            Mesajlar<USER> m = new clsUser_Proccess().Duzelt(User);

            return Json(m);
        }

        [HttpGet("User_Delete")]
        public IActionResult User_Delete(int UserID)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = uProc.Getir(x => x.ObjectID == UserID);

            if (m.Nesne != null)
            {
                m = uProc.Sil(m.Nesne);
            }

            return Json(m);
        }

        [HttpGet("User_Select")]
        public IActionResult User_Select(int UserID)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = uProc.Getir(x => x.ObjectID == UserID);

            return Json(m);
        }

        [HttpGet("User_List")]
        public IActionResult User_List()
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = uProc.Listele(x => x.Status == true);

            return Json(m);
        }
        [AllowAnonymous]
        [HttpPost("User_Login")]
        public IActionResult User_Login(USER_LOGIN User)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();

            Mesajlar<USER> m = uProc.Getir(x => x.Status == true && x.Email == User.UserName && x.Password == User.Password);

            return Json(m);
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Mesajlar<USER_LOGIN> User)
        {
            var token = _jwtAuthenticationManager.Authenticate(User);
            if (token==null)
            {
                return Unauthorized();
            }
            else
            {
                
                return Ok(User);
            }
        }



    }
}
