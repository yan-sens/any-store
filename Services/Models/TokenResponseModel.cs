using Common.Enums;

namespace Services.Models
{
    public class TokenResponseModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public long ExpiresIn { get; set; }

        public RolesEnum UserRole { get; set; }

        public string UserId { get; set; }
    }
}
