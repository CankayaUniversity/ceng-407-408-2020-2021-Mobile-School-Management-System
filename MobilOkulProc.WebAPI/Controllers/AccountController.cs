using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserMgr { get; }
        private SignInManager<AppUser> SignInMgr { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                ViewBag.Message = "User already registered";
                AppUser user = await UserMgr.FindByNameAsync(registerModel.Username);
                if (user == null)
                {
                    user = new AppUser();
                    user.UserName = registerModel.Username;
                    user.Email = registerModel.Username;
                    user.FirstName = registerModel.FirstName;
                    user.LastName = registerModel.LastName;
                    IdentityResult result = await UserMgr.CreateAsync(user, registerModel.Password);
                    ViewBag.Message = "User was created";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                
            }
            return Ok(ViewBag.Message.ToString());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] USER_LOGIN m)
        {
            var result = await SignInMgr.PasswordSignInAsync(m.UserName, m.Password, false, false);
            if (result.Succeeded)
            {
                
                return Json(m);
            }
            else
            {
                ViewBag.Result = "result is: " + result.ToString();
                return Unauthorized(m);
            }
        }
    }
}
