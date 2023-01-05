
using Application.Features.OperationClaims.DTOs;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim;

public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        await _operationClaimBusinessRules.OperationClaimMustExistWhenRequested(request.Id);
        await _operationClaimBusinessRules.OperationClaimNameCannotBeDuplicated(request.Name);
        var mappedClaim = _mapper.Map<OperationClaim>(request);
        var updatedClaim = await _operationClaimRepository.UpdateAsync(mappedClaim);
        return _mapper.Map<UpdatedOperationClaimDto>(updatedClaim);
    }
}
