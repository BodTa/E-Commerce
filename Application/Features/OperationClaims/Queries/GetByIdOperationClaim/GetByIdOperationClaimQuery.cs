

using Application.Features.OperationClaims.DTOs;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetByIdOperationClaim;

public class GetByIdOperationClaimQuery : IRequest<OperationClaimDto>
{
    public int Id { get; set; }
}

public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public GetByIdOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<OperationClaimDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
    {
        await _operationClaimBusinessRules.OperationClaimMustExistWhenRequested(request.Id);
        var claim = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);
        return _mapper.Map<OperationClaimDto>(claim);
    }
}
