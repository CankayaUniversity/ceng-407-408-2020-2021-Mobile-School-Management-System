using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using MobilOkulProc.WebAPI.Models;
using MobilOkulProc.WebAPI.Services;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MobilOkulProc.WebAPI.Helpers;
using AutoMapper;

namespace MobilOkulProc.WebAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        //private IJWTAuthenticationManager _jwtAuthenticationManager;
        //public UserController(IJWTAuthenticationManager jwtAuthenticationManager)
        //{
        //    _jwtAuthenticationManager = jwtAuthenticationManager;
        //}
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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
        public IActionResult Authenticate([FromBody]AuthenticateRequest model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<zUser>(model);

            try
            {
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<zUser>(model);
            user.Id = id;

            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }



    }
}
