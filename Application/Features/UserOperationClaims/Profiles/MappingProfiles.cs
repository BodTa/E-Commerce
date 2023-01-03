

using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaims.DTOs;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<UserOperationClaim,CreatedUserOperationClaimDto>().ReverseMap();
		CreateMap<UserOperationClaim,UpdatedUserOperationClaimDto>().ReverseMap();
		CreateMap<UserOperationClaim,DeletedUserOperationClaimDto>().ReverseMap();
		CreateMap<UserOperationClaim,CreateUserOperationClaimCommand>().ReverseMap();
		CreateMap<UserOperationClaim,UpdateUserOperationClaimCommand>().ReverseMap();
		CreateMap<UserOperationClaim,DeleteUserOperationClaimCommand>().ReverseMap();
		CreateMap<UserOperationClaim,UserOperationClaimDto>().ReverseMap();
		CreateMap<UserOperationClaim,UserOperationClaimListDto>().ReverseMap();
    }
}
