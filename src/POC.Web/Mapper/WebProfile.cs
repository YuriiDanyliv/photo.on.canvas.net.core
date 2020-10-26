using AutoMapper;
using POC.BLL.DTO;
using POC.Web.ViewModel;

namespace POC.Web.Mapper
{
  public class WebProfile: Profile 
  {  
    public WebProfile()
    {
      CreateMap<LoginViewModel, UserAuthDTO>();
      CreateMap<RegisterViewModel, UserAuthDTO>();

      CreateMap<OrderViewModel, OrderDTO>();

      CreateMap<CanvasViewModel, CanvasDTO>();
      CreateMap<CanvasDTO, CanvasViewModel>()
      .ForMember("Name", opt => opt.MapFrom(
        i => "Розмір: " + i.Size + ", Ціна: " + i.Price.ToString("C"))
      );
    }
  }
}