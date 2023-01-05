

using Application.Features.OperationClaims.DTOs;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>
{
    public int Id { get; set; }
}

public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public DeleteOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
    {
        await _operationClaimBusinessRules.OperationClaimMustExistWhenRequested(request.Id);
        var mappedClaim = _mapper.Map<OperationClaim>(request);
        var deletedClaim = await _operationClaimRepository.DeleteAsync(mappedClaim);
        return _mapper.Map<DeletedOperationClaimDto>(deletedClaim);
    }
}
