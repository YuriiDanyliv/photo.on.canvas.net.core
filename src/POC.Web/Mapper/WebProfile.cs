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
    }
  }
}