using System;
using System.Collections.Generic;
using System.Text;

namespace MobilOkulProc.Entities.Concrete
{
    public class zUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
