

using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task OperationClaimMustExistWhenRequested(int id)
    {
        var claim = await _operationClaimRepository.GetAsync(o => o.Id == id);
        if (claim == null) throw new BusinessException("Operation Claim does not exists.");
    }

    public async Task OperationClaimNameCannotBeDuplicated(string name)
    {
        var claim = await _operationClaimRepository.GetAsync(o => o.Name == name);
        if (claim != null) throw new BusinessException("Operation Claim Name cannot be duplicated.");
    }
}
