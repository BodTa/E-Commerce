

using Application.Features.Companies.DTOs;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommand : IRequest<DeletedCompanyDto>
{
    public int Id { get; set; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, DeletedCompanyDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyBusinessRules _companyBusinessRules;

    public DeleteCompanyCommandHandler(IMapper mapper, ICompanyRepository companyRepository, CompanyBusinessRules companyBusinessRules)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _companyBusinessRules = companyBusinessRules;
    }

    public async Task<DeletedCompanyDto> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        await _companyBusinessRules.CompanyShouldExistWhenRequested(request.Id);
        var company = _mapper.Map<Company>(request);
        var deletedComapny = await _companyRepository.DeleteAsync(company);
        return _mapper.Map<DeletedCompanyDto>(deletedComapny);
    }
}
