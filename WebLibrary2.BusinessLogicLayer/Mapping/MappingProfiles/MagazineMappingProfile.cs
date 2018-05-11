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
    public class MagazineMappingProfile : Profile
    {
        public MagazineMappingProfile()
        {
            CreateMap<Magazine, GetMagazineView>().ReverseMap();
            CreateMap<Magazine, GetAllMagazinesView>().ForMember(dest => dest.MagazineGenreID,opt => opt.MapFrom(src => src.MagazineGenres.MagazineGenreID)).
                                                       ForMember(dest => dest.MagazineGenreName,opt => opt.MapFrom(src => src.MagazineGenres.MagazineGenreName)).ReverseMap();
        }
    }
}
