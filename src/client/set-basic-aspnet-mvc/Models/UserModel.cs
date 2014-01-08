using System;

using set_basic_aspnet_mvc.Domain.Entities;
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

        public bool IsValidNewUser()
        {
            return !string.IsNullOrEmpty(Password)
                   && !string.IsNullOrEmpty(FullName)
                   && Email.IsEmail();
        }

        public static UserModel Map(User user)
        {
            var model = new UserModel
            {
                Id = user.Id,
                Email = user.Email,                
                FullName = user.FullName,
                RoleName = user.RoleName,
                Language = user.Language,
                IsActive = user.IsActive,
            };
            return model;
        }

    }
}