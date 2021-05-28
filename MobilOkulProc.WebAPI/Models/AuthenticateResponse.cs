using MobilOkulProc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(int _Id,string _FirstName, string _LastName, string _Username, string token)
        {
            Id = _Id;
            FirstName = _FirstName;
            LastName = _LastName;
            Username = _Username;
            Token = token;
        }
    }
}
