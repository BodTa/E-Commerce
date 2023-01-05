

using Application.Features.Companies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Companies.Queries.GetListByCityCompany;

public class GetListByCityCompanyQuery : IRequest<CompanyListModel>
{
    public City City { get; set; }
    public PageRequest PageRequest { get; set; }
}

public class GetListByCityCompanyQueryHandler : IRequestHandler<GetListByCityCompanyQuery, CompanyListModel>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public GetListByCityCompanyQueryHandler(IMapper mapper, ICompanyRepository companyRepository)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    public async Task<CompanyListModel> Handle(GetListByCityCompanyQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Company> companies = await _companyRepository.GetListAsync(c => c.City == request.City,
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<CompanyListModel>(companies);
    }
}
