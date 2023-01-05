

using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.CompanyService;

public class CompanyManager : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyManager(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

}
