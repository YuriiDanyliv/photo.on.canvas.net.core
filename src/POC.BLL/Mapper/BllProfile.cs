using System.Linq;
using AutoMapper;
using POC.BLL.DTO;
using POC.DAL.Entities;

namespace POC.BLL.Mapper
{
  public class BllProfile : Profile
  {
    public BllProfile()
    {
      CreateMap<User, UserDTO>();
      CreateMap<UserAuthDTO, User>();
      CreateMap<OrderDTO, Order>();
      CreateMap<CanvasDTO, Canvas>();
    }
  }
}