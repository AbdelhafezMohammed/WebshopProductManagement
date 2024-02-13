using AutoMapper;
using WebShop.Data.Models;
using WebShop.Service.Dtos;

namespace WebShop.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
