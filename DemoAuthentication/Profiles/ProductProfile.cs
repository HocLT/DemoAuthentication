using AutoMapper;
using DemoAuthentication.Dto;
using DemoAuthentication.Models;

namespace DemoAuthentication.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductCreateDto>();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
