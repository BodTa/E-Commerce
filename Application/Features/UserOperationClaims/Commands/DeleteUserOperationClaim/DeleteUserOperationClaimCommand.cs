

using Application.Features.UserOperationClaims.DTOs;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;

public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimDto>
{
    public int Id { get; set; }
}

public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public DeleteUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        await _userOperationClaimBusinessRules.UserOperationClaimShouldBeExistWhenRequested(request.Id);
        var mappedClaim = _mapper.Map<UserOperationClaim>(request);
        var deletedClaim = await _userOperationClaimRepository.DeleteAsync(mappedClaim);
        return _mapper.Map<DeletedUserOperationClaimDto>(deletedClaim);
    }
}
