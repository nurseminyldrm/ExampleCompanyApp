using AutoMapper;
using ExampleCompanyApp.Core.Dtos;
using ExampleCompanyApp.Core.Models;
using System.Net.Http.Headers;

namespace ExampleCompanyApp.Service.Mapping;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Product, ProductWithCategoryDto>().ReverseMap();

        CreateMap<Category, CategoryWithProductDto>()
            .ForMember(dest => dest.ProductDto, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();

        //CreateMap<ProductFeatureDto, Product>().ReverseMap();

        CreateMap<ProductUpdateDto, Product>().ReverseMap();

    }
}
