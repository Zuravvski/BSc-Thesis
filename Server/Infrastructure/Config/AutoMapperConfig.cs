using AutoMapper;
using Core.Domain.Robots;

namespace Infrastructure.Config
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(config =>
            {
            }).CreateMapper();
        }
    }
}
