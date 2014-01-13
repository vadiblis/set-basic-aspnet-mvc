using AutoMapper;

using set_basic_aspnet_mvc.Domain.DataTransferObjects;
using set_basic_aspnet_mvc.Domain.Entities;

namespace set_basic_aspnet_mvc.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
        }
    }
}