using AutoMapper;
using POC.BLL.Dto;
using POC.DAL.Entities;

namespace POC.BLL.Mapper
{
    class InstagramProfile : Profile
    {
        public InstagramProfile()
        {
            CreateMap<InstagramMediaDto, InstagramMedia>();
        }
    }
}
