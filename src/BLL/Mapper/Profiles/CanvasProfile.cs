using AutoMapper;
using POC.BLL.Dto;
using POC.DAL.Entities;

namespace POC.BLL.Mapper
{
    class CanvasProfile : Profile
    {
        public CanvasProfile()
        {
            CreateMap<CreateCanvasDto, Canvas>();

            CreateMap<Canvas, CanvasDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => "Розмір: " + src.Size + ", Ціна: " + src.Price.ToString("C")));
        }
    }
}
