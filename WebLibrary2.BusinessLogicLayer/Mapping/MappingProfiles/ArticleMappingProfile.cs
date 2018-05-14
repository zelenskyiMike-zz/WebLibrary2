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
    class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<Article, GetArticleView>().ReverseMap();
            CreateMap<Article, GetAllArticlesView>().ForMember(dest => dest.ArticleGenreID, opt => opt.MapFrom(src => src.ArticleGenres.ArticleGenreID)).
                                                    ForMember(dest => dest.ArticleGenreName, opt => opt.MapFrom(src => src.ArticleGenres.ArticleGenreName)).ReverseMap();
        }
    }
}
