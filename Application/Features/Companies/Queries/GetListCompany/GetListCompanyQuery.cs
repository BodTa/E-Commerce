
using Application.Features.Companies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Queries.GetListCompany;

public class GetListCompanyQuery : IRequest<CompanyListModel>
{
    public PageRequest  PageRequest { get; set; }
}

public class GetListCompanyQueryHandler : IRequestHandler<GetListCompanyQuery, CompanyListModel>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public GetListCompanyQueryHandler(IMapper mapper, ICompanyRepository companyRepository)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    public async Task<CompanyListModel> Handle(GetListCompanyQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Company> companies = await _companyRepository.GetListAsync(index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<CompanyListModel>(companies);
    }
}
