
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Companies.Rules;

public class CompanyBusinessRules
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyBusinessRules(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task CompanyShouldExistWhenRequested(int id)
    {
        var company = await _companyRepository.GetAsync(c => c.Id == id);
        if (company == null) throw new BusinessException("Company does not exists");
    }

    public async Task CompanyNameCannotBeDuplicated(string name)
    {
        var company = await _companyRepository.GetAsync(c => c.Name == name);
        if (company != null) throw new BusinessException("Company name cannot be duplicated");
    }
}
