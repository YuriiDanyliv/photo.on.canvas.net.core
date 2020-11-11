using AutoMapper;
using POC.BLL.DTO;
using POC.BLL.Model;
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

      CreateMap<CreateOrderVM, CreateOrder>();

      CreateMap<Canvas, CanvasResponseVM>()
      .ForMember
      (
        dest => dest.Name,
        opt => opt.MapFrom(src => "Розмір: " + src.Size + ", Ціна: " + src.Price.ToString("C"))
      );

      CreateMap<PagesList<UserDTO>, PagesVM<UserDTO>>()
      .ForMember
      (
        dest => dest.data,
        opt => opt.MapFrom(src => src.ToArray())
      );

      CreateMap<PagesList<Order>, PagesVM<Order>>()
      .ForMember
      (
        dest => dest.data,
        opt => opt.MapFrom(src => src.ToArray())
      );
    }
  }
}