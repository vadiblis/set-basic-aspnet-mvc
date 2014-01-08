using System;
using System.Data.Entity;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Repositories
{
    public class SetDbInitializer : DropCreateDatabaseIfModelChanges<SetDbContext>
    {
        protected override void Seed(SetDbContext context)
        {
         
            AddAdmin(context, "Serdar Büyüktemiz", "hserdarb@gmail.com");
            AddAdmin(context, "Caner Çavuş", "canercvs@gmail.com");
            AddAdmin(context, "Ramiz Sümer", "ramiz.sumerr@gmail.com");
            AddAdmin(context, "Mehmet Sabancıoğlu", "mehmet.sabancioglu@gmail.com");
            AddAdmin(context, "Cihan Çoşkun", "cihancoskun@gmail.com");


            AddUser(context, "Kemal Çolak", "kml.colak@gmail.com");
          



            context.SaveChanges();
        }

        private static void AddAdmin(SetDbContext context, string name, string email)
        {
            var user = new User
            {
                Email = email,
                FullName = name,
                RoleId = SetRole.Admin.Value,
                RoleName = SetRole.Admin.ToString(),
                ImageUrl = GravatarHelper.GetGravatarURL(email, 35, "mm"),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                LastLoginAt = DateTime.Now,
                IsActive = true
            };
            context.Users.Add(user);
        }
        private static void AddUser(SetDbContext context, string name, string email)
        {
            var user = new User
            {
                Email = email,
                FullName = name,
                RoleId = SetRole.User.Value,
                RoleName = SetRole.User.ToString(),
                ImageUrl = GravatarHelper.GetGravatarURL(email, 35, "mm"),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                LastLoginAt = DateTime.Now,
                IsActive = true
            };
            context.Users.Add(user);
        }

    }
}