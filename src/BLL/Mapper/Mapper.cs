using AutoMapper;

namespace POC.BLL.Mapper
{
  public class ObjMapper
  {
    private static readonly IMapper mapper;
    public static readonly MapperConfiguration configuration;

    static ObjMapper()
    {
      configuration = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<BllProfile>();
      });

      mapper = configuration.CreateMapper();
    }

    public static Destination Map<Source, Destination>(Source source) => mapper.Map<Destination>(source);
  }
}