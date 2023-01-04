

using Application.Features.Companies.Commands.CreateCompany;
using Application.Features.Companies.Commands.DeleteCompany;
using Application.Features.Companies.Commands.UpdateCompany;
using Application.Features.Companies.DTOs;
using Application.Features.Companies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Companies.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<Company,CreatedCompanyDto>().ReverseMap();
		CreateMap<Company,UpdatedCompanyDto>().ReverseMap();
		CreateMap<Company,DeletedCompanyDto>().ReverseMap();
		CreateMap<Company,CreateCompanyCommand>().ReverseMap();
		CreateMap<Company,UpdateCompanyCommand>().ReverseMap();
		CreateMap<Company,DeleteCompanyCommand>().ReverseMap();
		CreateMap<Company,CompanyDto>().ReverseMap();
		CreateMap<Company, CompanyListDto>().ReverseMap();
		CreateMap<IPaginate<Company>, CompanyListModel>().ReverseMap();
    }
}
