using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Models
{
    public class PasswordResetModel : BaseModel
    {
        public string Email { get; set; }

        public override bool IsValid()
        {
            return Email.IsEmail();
        }
    }
}