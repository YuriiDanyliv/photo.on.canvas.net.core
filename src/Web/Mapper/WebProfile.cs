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
      
      CreateMap<Order, OrderResponseVM>();

      CreateMap<Canvas, CanvasResponseVM>()
      .ForMember( dest => dest.Name, opt => opt.MapFrom(src => "Розмір: " + src.Size + ", Ціна: " + src.Price.ToString("C")));

      CreateMap<PagesList<UserDTO>, PagesVM<UserDTO>>()
      .ForMember( dest => dest.data, opt => opt.MapFrom(src => src.ToArray()));

      CreateMap<PagesList<Order>, PagesVM<OrderResponseVM>>()
      .ForMember( dest => dest.data, opt => opt.MapFrom(src => src.ToArray()));

      CreateMap<FileData, GalleryGetImageVM>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.FileName))
      .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType))
      .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.File));

    }
  }
}