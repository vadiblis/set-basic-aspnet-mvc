using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Models
{
    public class UserModel : BaseModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public long Id { get; set; }
        public string RoleName { get; set; }
        public string Language { get; set; }
        public bool IsActive { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Password)
                   && !string.IsNullOrEmpty(FullName)
                   && Email.IsEmail();
        }
    }
}