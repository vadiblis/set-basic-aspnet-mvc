using System;
using System.Linq;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IUserService
    {
        //Task<User> GetByEmail(string email);
        //Task<List<User>> GetAll(int page, int latestId);
        //Task<List<User>> GetAllByRoleId(int roleId, int page, int latestId);

        Task<object> Create(string fullName, string email, string password, int roleId, string language);
        Task<bool> Authenticate(string email, string password);
        Task<bool> ChangeStatus(int userId, int updatedBy, bool isActive);

        Task<User> Get(int id);
        Task<User> GetByEmail(string email);
    }

    public class UserService : IUserService
    {
        const int LOGIN_TRY_COUNT = 5;

        private readonly IRepository<User> _userRepo;
        public UserService(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<object> Create(string fullName, string email, string password, int roleId, string language)
        {
            var img = GravatarHelper.GetGravatarURL(email, 55, "mm");
            var user = new User
            {
                Email = email,
                FullName = fullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                ImageUrl = img,
                RoleId = roleId,
                RoleName = SetRole.GetString(roleId),
                IsActive = true,
                Language = language
            };                                   

            _userRepo.Create(user);

            if (!_userRepo.SaveChanges()) return null;

            return await Task.FromResult(user.Id);
        }

        public Task<bool> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(password) && email.IsEmail()) return Task.FromResult(false);

            var user = _userRepo.FindOne(x => x.Email == email 
                                              && x.PasswordHash != null 
                                              && x.LoginTryCount < LOGIN_TRY_COUNT);
            if (user == null) return Task.FromResult(false);

            var result = false;

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)
                && user.LoginTryCount < LOGIN_TRY_COUNT)
            {
                user.LastLoginAt = DateTime.Now;
                user.LoginTryCount = 0;
                result = true;
            }
            else
            {
                user.LoginTryCount += 1;
            }

            _userRepo.Update(user);

            if (!_userRepo.SaveChanges()) Task.FromResult(false);

            return Task.FromResult(result);
        }

        public Task<bool> ChangeStatus(int userId, int updatedBy, bool isActive)
        {
            if (userId < 1 || updatedBy < 1) return Task.FromResult(false);

            var user = _userRepo.FindOne(x => x.Id == userId);
            if (user == null) return Task.FromResult(false);

            var updatedByUser = _userRepo.FindOne(x => x.Id == updatedBy);
            if (updatedByUser == null) return Task.FromResult(false);

            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = updatedBy;
            user.IsActive = !isActive;
            _userRepo.Update(user);

            if (!_userRepo.SaveChanges()) Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="roleId">default is 3 - SetLocaleRole.Developer.Value </param>
        ///// <returns></returns>
        //public async Task<int?> Create(UserModel model, int roleId = 3)
        //{
        //    var img = GravatarHelper.GetGravatarURL(model.Email, 55, "mm");
        //    var user = new User
        //    {
        //        Email = model.Email,
        //        Name = model.Name,
        //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
        //        ImageUrl = img,
        //        RoleId = roleId,
        //        RoleName = SetLocaleRole.GetString(roleId),
        //        IsActive = true,
        //        Language = model.Language
        //    };
        //    _userRepo.Create(user);



        //    if (!_userRepo.SaveChanges()) return null;

        //    return await Task.FromResult(user.Id);
        //}

        //public Task<User> GetByEmail(string email)
        //{
        //    if (!email.IsEmail())
        //    {
        //        return null;
        //    }

        //    var user = _userRepo.FindOne(x => x.Email == email);
        //    return Task.FromResult(user);
        //}

        //public Task<bool> Authenticate(string email, string password)
        //{
        //    var user = _userRepo.FindOne(x => x.Email == email && x.PasswordHash != null);
        //    if (user == null) return Task.FromResult(false);
        //    var result = false;

        //    if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)
        //        && user.LoginTryCount < 5)
        //    {
        //        user.LastLoginAt = DateTime.Now;
        //        user.LoginTryCount = 0;
        //        result = true;
        //    }
        //    else
        //    {
        //        user.LoginTryCount += 1;
        //    }

        //    _userRepo.Update(user);
        //    _userRepo.SaveChanges();

        //    return Task.FromResult(result);
        //}

        //public Task<List<User>> GetAll()
        //{
        //    var users = _userRepo.FindAll().ToList();
        //    return Task.FromResult(users);
        //}

        //public Task<List<User>> GetAllByRoleId(int roleId)
        //{
        //    var users = _userRepo.FindAll(x => x.RoleId == roleId).ToList();
        //    return Task.FromResult(users);
        //}

        //public Task<bool> ChangeStatus(int userId, bool isActive)
        //{
        //    if (userId < 1)
        //    {
        //        return Task.FromResult(false);
        //    }

        //    var user = _userRepo.FindOne(x => x.Id == userId);
        //    if (user == null)
        //    {
        //        return Task.FromResult(false);
        //    }

        //    user.IsActive = !isActive;
        //    _userRepo.Update(user);
        //    _userRepo.SaveChanges();

        //    return Task.FromResult(true);
        //}



        
    }
}