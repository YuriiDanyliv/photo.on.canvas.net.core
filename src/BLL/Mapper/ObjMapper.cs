using AutoMapper;
using POC.BLL.Mapper;

namespace BLL.Mapper
{
    public static class ObjMapper
    {
        private static readonly IMapper mapper;
        public static readonly MapperConfiguration configuration;

        static ObjMapper()
        {
            configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CanvasProfile>();
                cfg.AddProfile<InstagramProfile>();
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<PagesListProfile>();
                cfg.AddProfile<UserProfile>();
            });

            mapper = configuration.CreateMapper();
        }

        public static Destination Map<Source, Destination>(Source source) => mapper.Map<Destination>(source);
    }
}
