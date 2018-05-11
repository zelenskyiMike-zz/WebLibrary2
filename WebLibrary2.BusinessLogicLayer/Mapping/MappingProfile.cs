using AutoMapper;
using WebLibrary2.BusinessLogicLayer.Mapping.MappingProfiles;

namespace WebLibrary2.BusinessLogicLayer.Mapping
{
    public class MappingProfile
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile<AuthorMappingProfile>();
                cfg.AddProfile<BookMappingProfile>();
                //cfg.AddProfile<BookGenresMappingProfile>();
                //cfg.AddProfile<ArticleMappingProfile>();
                //cfg.AddProfile<MagazineMappingProfile>();
            });
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}

