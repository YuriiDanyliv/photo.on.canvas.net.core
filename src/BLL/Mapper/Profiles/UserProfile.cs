using AutoMapper;
using POC.BLL.Dto;
using POC.DAL.Entities;

namespace POC.BLL.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserAuthDto, User>();
        }
    }
}