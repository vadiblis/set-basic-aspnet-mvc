using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Models
{
    public class PasswordChangeModel : BaseModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Password)
                   && !string.IsNullOrEmpty(Token)
                   && !string.IsNullOrEmpty(Email)
                   && Email.IsEmail();
        }
    }
}