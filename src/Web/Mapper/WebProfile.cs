using AutoMapper;
using POC.BLL.DTO;
using POC.DAL.Entities;
using POC.DAL.Models;
using POC.Web.ViewModel;

namespace POC.Web.Mapper
{
  public class WebProfile : Profile
  {
    public WebProfile()
    {
      CreateMap<LoginViewModel, UserAuthDTO>();

      CreateMap<RegisterViewModel, UserAuthDTO>();

      CreateMap<OrderViewModel, OrderDTO>();

      CreateMap<CanvasViewModel, CanvasDTO>();

      CreateMap<Canvas, CanvasViewModel>()
      .ForMember
      (
        dest => dest.Name,
        opt => opt.MapFrom(src => "Розмір: " + src.Size + ", Ціна: " + src.Price.ToString("C"))
      );
      
      CreateMap<CanvasDTO, CanvasViewModel>()
      .ForMember
      (
        dest => dest.Name,
        opt => opt.MapFrom(src => "Розмір: " + src.Size + ", Ціна: " + src.Price.ToString("C"))
      );

      CreateMap<PagesList<UserDTO>, PagesViewModel<UserDTO>>()
      .ForMember
      (
        dest => dest.data,
        opt => opt.MapFrom(src => src.ToArray())
      );
    }
  }
}