

using Application.Features.Companies.DTOs;
using Application.Services.ProductService;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Companies.Queries.GetByProductIdCompany;

public class GetByProductIdCompanyQuery : IRequest<CompanyDto>
{
    public int ProductId { get; set; }
}

public class GetByProductIdCompanyQueryHandler : IRequestHandler<GetByProductIdCompanyQuery, CompanyDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IProductService _productService;

    public GetByProductIdCompanyQueryHandler(IMapper mapper, ICompanyRepository companyRepository, IProductService productService)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _productService = productService;
    }

    public async Task<CompanyDto> Handle(GetByProductIdCompanyQuery request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductById(request.ProductId);
        return _mapper.Map<CompanyDto>(product.Company);
    }
}
