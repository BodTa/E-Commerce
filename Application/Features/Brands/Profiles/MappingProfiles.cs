

using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.DTOs;
using Application.Features.Brands.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<Brand,CreatedBrandDto>().ReverseMap();
		CreateMap<Brand,DeletedBrandDto>().ReverseMap();
		CreateMap<Brand,UpdatedBrandDto>().ReverseMap();
        CreateMap<Brand,CreateBrandCommand>().ReverseMap();
        CreateMap<Brand,DeleteBrandCommand>().ReverseMap();
        CreateMap<Brand, UpdateBrandCommand>().ReverseMap();
        CreateMap<Brand,BrandDto>().ReverseMap();
		CreateMap<Brand,BrandListDto>().ReverseMap();
        CreateMap<IPaginate<Brand>, BrandListModel>().ReverseMap();
    }
}
