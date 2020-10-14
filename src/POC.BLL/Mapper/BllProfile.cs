using System.Linq;
using AutoMapper;
using PCO.BLL.DTO;
using PCO.DAL.Entities;

namespace PCO.BLL.Mapper
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