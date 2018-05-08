using AutoMapper;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BLL.Mapping.MappingProfiles
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, GetAuthorView>().ReverseMap();
        }
    }
}
