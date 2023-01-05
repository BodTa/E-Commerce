
using Application.Features.Companies.DTOs;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Queries.GetByIdCompany;

public class GetByIdCompanyQuery : IRequest<CompanyDto>
{
    public int Id { get; set; }
}

public class GetByIdCompanyQueryHandler : IRequestHandler<GetByIdCompanyQuery, CompanyDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyBusinessRules _companyBusinessRules;

    public GetByIdCompanyQueryHandler(IMapper mapper, ICompanyRepository companyRepository, CompanyBusinessRules companyBusinessRules)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyBusinessRules = companyBusinessRules;
    }

    public async Task<CompanyDto> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
    {
        await _companyBusinessRules.CompanyShouldExistWhenRequested(request.Id);
        var company = await _companyRepository.GetAsync(c=> c.Id == request.Id);
        return _mapper.Map<CompanyDto>(company);
    }
}
