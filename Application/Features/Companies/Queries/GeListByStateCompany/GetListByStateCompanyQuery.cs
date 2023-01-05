

using Application.Features.Companies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Companies.Queries.GetByStateCompany;

public class GetListByStateCompanyQuery : IRequest<CompanyListModel>
{
    public CompanyState  CompanyState { get; set; }
    public PageRequest PageRequest { get; set; }
}

public class GetListByStateCompanyQueryHandler : IRequestHandler<GetListByStateCompanyQuery, CompanyListModel>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public GetListByStateCompanyQueryHandler(IMapper mapper, ICompanyRepository companyRepository)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    public async Task<CompanyListModel> Handle(GetListByStateCompanyQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Company> companies = await _companyRepository.GetListAsync(c=>c.State == request.CompanyState,
            index: request.PageRequest.Page, size: request.PageRequest.PageSize);
        return _mapper.Map<CompanyListModel>(companies);
    }
}