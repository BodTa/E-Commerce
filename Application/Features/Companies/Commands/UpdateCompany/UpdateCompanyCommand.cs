
using Application.Features.Companies.DTOs;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommand : IRequest<UpdatedCompanyDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CompanyState CompanyState { get; set; }
    public City City { get; set; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, UpdatedCompanyDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyBusinessRules _companyBusinessRules;

    public UpdateCompanyCommandHandler(IMapper mapper, ICompanyRepository companyRepository, CompanyBusinessRules companyBusinessRules)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyBusinessRules = companyBusinessRules;
    }

    public async Task<UpdatedCompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        await _companyBusinessRules.CompanyShouldExistWhenRequested(request.Id);
        await _companyBusinessRules.CompanyNameCannotBeDuplicated(request.Name);
        var company = _mapper.Map<Company>(request);
        var updatedCompany = await _companyRepository.UpdateAsync(company);
        return _mapper.Map<UpdatedCompanyDto>(updatedCompany);
    }
}