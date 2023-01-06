
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task UserOperationClaimShouldBeExistWhenRequested(int id)
    {
        var claim = await _userOperationClaimRepository.GetAsync(u => u.Id == id);
        if (claim == null) throw new BusinessException("User Operation Claim does not exists");
    }
}
