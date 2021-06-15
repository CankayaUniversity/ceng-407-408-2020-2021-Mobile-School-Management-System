using System.Text.Json.Serialization;



namespace MobilOkulProc.WebAPI.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string Role { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(int Id,string FirstName, string LastName,string Username, string jwtToken, string refreshToken, string role)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = Username;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            Role = role;
        }
    }
}

