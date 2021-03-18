using AutoMapper;
using POC.BLL.Dto;
using POC.DAL.Entities;
using System.IO;

namespace POC.BLL.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<IFormFileToByteArrResolver>());
        }
    }

    public class IFormFileToByteArrResolver : IValueResolver<CreateOrderDto, Order, byte[]>
    {
        public byte[] Resolve(CreateOrderDto source, Order destination, byte[] destMember, ResolutionContext context)
        {
            using (var binaryReader = new BinaryReader(source.Image.OpenReadStream()))
            {
                return binaryReader.ReadBytes((int)source.Image.Length);
            }
        }
    }
}