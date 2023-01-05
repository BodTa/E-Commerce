

using Application.Features.OperationClaims.DTOs;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>
{
    public string Name { get; set; }
}

public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public CreateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        await _operationClaimBusinessRules.OperationClaimNameCannotBeDuplicated(request.Name);
        var mappedClaim = _mapper.Map<OperationClaim>(request);
        var createdClaim = await _operationClaimRepository.AddAsync(mappedClaim);
        return _mapper.Map<CreatedOperationClaimDto>(createdClaim);
    }
}
