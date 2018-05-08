using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.BLL.Mapping.MappingProfiles;

namespace WebLibrary2.BLL.Mapping
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AuthorMappingProfile());
                //cfg.AddProfile(new MagazineMappingProfile());
                //cfg.AddProfile(new BookMappingProfile());
                //cfg.AddProfile(new PublicHouseMappingProfile());
                //cfg.AddProfile(new LibraryMappingProfile());
            });

            return config;
        }
    }
}

