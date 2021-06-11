using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MobilOkulProc.WebAPI.Entities
{
    public class zUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
