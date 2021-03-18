using AutoMapper;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Models;

namespace POC.BLL.Mapper
{
    public class PagesListProfile : Profile
    {
        public PagesListProfile()
        {
            CreateMap<PagesList<UserDto>, PagesListModel<UserDto>>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.ToArray()));

            CreateMap<PagesList<Order>, PagesListModel<OrderDto>>()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.ToArray()));
        }
    }
}
