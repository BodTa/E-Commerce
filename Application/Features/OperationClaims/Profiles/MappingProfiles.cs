

using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.DTOs;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<OperationClaim,CreatedOperationClaimDto>().ReverseMap();
		CreateMap<OperationClaim,UpdatedOperationClaimDto>().ReverseMap();
		CreateMap<OperationClaim,DeletedOperationClaimDto>().ReverseMap();
		CreateMap<OperationClaim,CreateOperationClaimCommand>().ReverseMap();
		CreateMap<OperationClaim,UpdateOperationClaimCommand>().ReverseMap();
		CreateMap<OperationClaim,DeleteOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim,OperationClaimDto>().ReverseMap();
        CreateMap<OperationClaim,OperationClaimListDto>().ReverseMap();

    }
}
