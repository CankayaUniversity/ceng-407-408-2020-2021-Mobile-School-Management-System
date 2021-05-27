using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.Entities.General;
using MobilOkulProc.WebAPI.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace MobilOkulProc.WebAPI
{

    public class JwtAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string _key;
        public JwtAuthenticationManager(string key)
        {
            _key = key;
        }

       
        public Mesajlar<USER_LOGIN> Authenticate(Mesajlar<USER_LOGIN> User)
        {
            clsUser_Proccess uProc = new clsUser_Proccess();
            Mesajlar<USER> user = uProc.Getir(x => x.Status == true && x.Email == User.Nesne.UserName && x.Password == User.Nesne.Password);
            if (user.Nesne == null)
            {
                return null;
            }
            var token = generateJwtToken(User);
            return null;
        }
        private string generateJwtToken(Mesajlar<USER_LOGIN> user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Nesne.UserName) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }



}
