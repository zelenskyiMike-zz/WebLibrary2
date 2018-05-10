﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Mapping.MappingProfiles
{
    public class BookGenresMappingProfile : Profile
    {
        public BookGenresMappingProfile()
        {
            CreateMap<BookGenre, GetBookGenreView>().ReverseMap();
        }
    }
}
