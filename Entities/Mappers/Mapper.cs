using AutoMapper;

namespace Entities.Mappers
{
    public static class Mapper
    {
        public static readonly IMapper Instance = GetMapper();

        private static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile<UserProfile>();
            });
            return mapperConfig.CreateMapper();
        }
    }
}