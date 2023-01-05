

using Application.Features.Companies.DTOs;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Companies.Commands.CreateCompany;

public class CreateCompanyCommand : IRequest<CreatedCompanyDto>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CompanyState State { get; set; }
    public DateTime JoinTime { get; set; }
    public City City { get; set; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreatedCompanyDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyBusinessRules _companyBusinessRules;

    public CreateCompanyCommandHandler(IMapper mapper, ICompanyRepository companyRepository, CompanyBusinessRules companyBusinessRules)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyBusinessRules = companyBusinessRules;
    }

    public async Task<CreatedCompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        await _companyBusinessRules.CompanyNameCannotBeDuplicated(request.Name);
        var mappedCompany = _mapper.Map<Company>(request);
        var createdCompany = await _companyRepository.AddAsync(mappedCompany);
        return _mapper.Map<CreatedCompanyDto>(createdCompany);
    }
}
