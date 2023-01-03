

using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<Product,CreatedProductDto>().ReverseMap();
		CreateMap<Product,UpdatedProductDto>().ReverseMap();
		CreateMap<Product,DeletedProductDto>().ReverseMap();
		CreateMap<Product,CreateProductCommand>().ReverseMap();
		CreateMap<Product,UpdateProductCommand>().ReverseMap();
		CreateMap<Product,DeleteProductCommand>().ReverseMap();
		CreateMap<Product,ProductDto>().ReverseMap();
		CreateMap<Product, ProductListDto>().ForMember(p => p.BrandName, opt => opt.MapFrom(p => p.Brand.Name))
			.ForMember(p => p.CompanyName, opt => opt.MapFrom(p => p.Company.Name))
			.ForMember(p => p.ColorName, opt => opt.MapFrom(p => p.Color.Name))
			.ForMember(p => p.CategoryName, opt => opt.MapFrom(p => p.Category.Name));

    }
}
