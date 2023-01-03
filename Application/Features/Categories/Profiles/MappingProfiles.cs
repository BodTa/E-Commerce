

using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<Category,CreatedCategoryDto>().ReverseMap();
        CreateMap<Category,UpdatedCategoryDto>().ReverseMap();
        CreateMap<Category,DeletedCategoryDto>().ReverseMap();
        CreateMap<Category,CategoryDto>().ReverseMap();
        CreateMap<Category,CreateCategoryCommand>().ReverseMap();
        CreateMap<Category,DeleteCategoryCommand>().ReverseMap();
        CreateMap<Category,UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, GetListCategoryDto>().ReverseMap();
    }
}
