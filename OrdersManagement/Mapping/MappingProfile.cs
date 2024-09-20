﻿using AutoMapper;
using DAL.Entities;
using PL.Models;

namespace PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ProductCategoryViewModel, ProductCategory>().ReverseMap();
            CreateMap<ProductPriceHistoryViewModel, ProductPriceHistory>().ReverseMap();
        }
    }
}
