

using Application.Features.UserOperationClaims.DTOs;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetByIdUserOperationClaim;

public class GetByIdUserOperationClaimQuery : IRequest<UserOperationClaimDto>
{
    public int Id { get; set; }
}

public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQuery, UserOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public GetByIdUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<UserOperationClaimDto> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
    {
        await _userOperationClaimBusinessRules.UserOperationClaimShouldBeExistWhenRequested(request.Id);
        var claim = await _userOperationClaimRepository.GetAsync(u => u.Id == request.Id);
        return _mapper.Map<UserOperationClaimDto>(claim);
    }
}
