using AutoMapper;
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
            CreateMap<UploadFile, UploadFileViewModel>().ReverseMap();




            //CreateMap<ProductUploadFileViewModel, ProductUploadFile>()
            //        .ForMember(dest => dest.ImgFile, opt => opt.MapFrom(src => src.ImgFile.OpenReadStream()))
            //        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //        .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
    
}
