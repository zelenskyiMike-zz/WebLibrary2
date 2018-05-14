using AutoMapper;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Mapping.MappingProfiles
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, GetAuthorView>().ReverseMap();
            CreateMap<Author, GetAuthorLiteratureView>().ReverseMap();
        }
    }
}
