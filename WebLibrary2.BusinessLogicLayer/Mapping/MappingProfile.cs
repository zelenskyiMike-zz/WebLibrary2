using AutoMapper;
using WebLibrary2.BusinessLogicLayer.Mapping.MappingProfiles;

namespace WebLibrary2.BLL.Mapping
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AuthorMappingProfile());
                cfg.AddProfile(new BookMappingProfile());
                cfg.AddProfile(new BookGenresMappingProfile());
                cfg.AddProfile(new ArticleMappingProfile());
            });

            return config;
        }
    }
}

