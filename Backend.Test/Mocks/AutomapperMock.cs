using AutoMapper;

namespace Backend.Test.Mocks
{
    public class AutomapperMock
    {
        public static IMapper GetMock()
        {
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            return mapper;
        }
    }
}