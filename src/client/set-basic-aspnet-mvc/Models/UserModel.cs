using System;

using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Models
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public string Language { get; set; }

        public bool IsValidNewUser()
        {
            return !String.IsNullOrEmpty(Password)
                   && !String.IsNullOrEmpty(Email)
                   && !String.IsNullOrEmpty(FullName)
                   && Email.IsEmail();
        }


    }
}