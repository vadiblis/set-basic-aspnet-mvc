using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Domain.DataTransferObjects;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public class UserService : IUserService
    {
        const int LOGIN_TRY_COUNT = 5;
        const int PASSWORD_RESET_VALIDATION_MINUTES = -60;
        const int GRAVATAR_IMG_SIZE = 55;

        private readonly IRepository<User> _userRepo;
        public UserService(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<long?> Create(string fullName, string email, string password, int roleId, string language)
        {
            var img = GravatarHelper.GetGravatarURL(email, GRAVATAR_IMG_SIZE);
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

        public async Task<bool> IsEmailExists(string email)
        {
            var result = _userRepo.AsQueryable(null).Any(x => x.Email == email);
            return await Task.FromResult(result);
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
        

        public async Task<bool> RequestPasswordReset(string email)
        {
            if (string.IsNullOrEmpty(email)) return await Task.FromResult(false);

            var user = _userRepo.FindOne(x => x.Email == email);
            if (user == null) return await Task.FromResult(false);

            if (user.PasswordResetRequestedAt != null
                && user.PasswordResetRequestedAt.Value.AddMinutes(PASSWORD_RESET_VALIDATION_MINUTES * -1) > DateTime.Now) return await Task.FromResult(false);

            var token = Guid.NewGuid().ToString().Replace("-", string.Empty);

            user.PasswordResetToken = token;
            user.PasswordResetRequestedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = user.Id;

            _userRepo.Update(user);

            if (!_userRepo.SaveChanges()) await Task.FromResult(false);

            //_messagingService.SendMail(new MsgSendMailReqModel
            //            {
            //                ApiKey = model.ApiKey,
            //                Email = model.Text,
            //                Body = string.Format(Resource.PasswordResetMailContent,
            //                                     string.Format("{0}?email={1}&token={2}",
            //                                                   Resource.PasswordResetRoute,
            //                                                   model.Text, token)),
            //                Subject = Resource.PasswordResetMailSubject,
            //                MemberId = model.MemberId
            //            });

            return await Task.FromResult(true);
        }

        public async Task<bool> IsPasswordResetRequestValid(string email, string token)
        {
            if (string.IsNullOrEmpty(email)) return await Task.FromResult(false);

            var user = _userRepo.FindOne(x => x.Email == email);
            if (user == null) return await Task.FromResult(false);

            if (user.PasswordResetRequestedAt == null) return await Task.FromResult(false);

            if (user.PasswordResetToken != token
                && user.Email != email
                && user.PasswordResetRequestedAt.Value < DateTime.Now.AddMinutes(PASSWORD_RESET_VALIDATION_MINUTES)) return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        public async Task<bool> ChangePassword(string email, string token, string password)
        {
            var isValid = await IsPasswordResetRequestValid(email, token);
            if (!isValid) return await Task.FromResult(false);

            var user = _userRepo.FindOne(x => x.Email == email);

            user.PasswordResetToken = null;
            user.PasswordResetRequestedAt = null;
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = user.Id;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.LoginTryCount = 0;

            _userRepo.Update(user);

            if (!_userRepo.SaveChanges()) await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        
        public Task<bool> ChangeStatus(long userId, long updatedBy, bool isActive)
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


        public Task<UserDto> Get(long id)
        {
            var user = _userRepo.FindOne(x => x.Id == id);

            var result = Mapper.Map<User, UserDto>(user);
            return Task.FromResult(result);
        }

        public Task<UserDto> GetByEmail(string email)
        {
            if (!email.IsEmail()) return null;

            var user = _userRepo.FindOne(x => x.Email == email);

            var result = Mapper.Map<User, UserDto>(user);
            return Task.FromResult(result);
        }

        public Task<PagedList<UserDto>> GetUsers(int pageNumber)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            pageNumber--;

            var items = _userRepo.FindAll();

            long totalCount = items.Count();

            items = items.OrderByDescending(x => x.Id).Skip(ConstHelper.PageSize * pageNumber).Take(ConstHelper.PageSize);

            var result = items.Select(Mapper.Map<User, UserDto>).ToList();

            return Task.FromResult(new PagedList<UserDto>(pageNumber, ConstHelper.PageSize, totalCount, result));
        }

    }
}