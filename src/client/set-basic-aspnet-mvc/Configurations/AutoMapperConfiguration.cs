using AutoMapper;

using set_basic_aspnet_mvc.Domain.DataTransferObjects;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Models;

namespace set_basic_aspnet_mvc.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<UserDto, UserModel>();
            Mapper.CreateMap<UserModel, UserDto>();


        }
    }
}