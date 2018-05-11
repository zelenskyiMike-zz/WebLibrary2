using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Mapping.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, GetBookView>().ReverseMap();
            CreateMap<Book, GetAllBooksView>().ForMember(dest => dest.GenreID, opt => opt.MapFrom(src => src.Genres.GenreID)).
                                              ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genres.GenreName)).ReverseMap();
            CreateMap<IEnumerable<Book>, IEnumerable<GetAllBooksView>>().ReverseMap();
        }
    }
}
