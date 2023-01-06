

using Application.Features.Products.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetListByCompanyIdProduct;

public class GetListByCompanyIdProduct : IRequest<ProductListModel>
{
    public int CompanyId { get; set; }
    public PageRequest PageRequest{ get; set; }
}

public class GetListByCompanyIdProductHandler : IRequestHandler<GetListByCompanyIdProduct, ProductListModel>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetListByCompanyIdProductHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductListModel> Handle(GetListByCompanyIdProduct request, CancellationToken cancellationToken)
    {
        IPaginate < Product > products= await _productRepository.GetListAsync(p => p.CompanyId == request.CompanyId,
            include: p=>p.Include(p=>p.Brand)
            .Include(p=>p.Company)
            .Include(p=>p.Color)
            .Include(p=>p.Category),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<ProductListModel>(products);
    }
}
