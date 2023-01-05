

using Application.Features.Products.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetListProduct;

public class GetListProductQuery : IRequest<ProductListModel>
{
    public PageRequest PageRequest{ get; set; }
}

public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, ProductListModel>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetListProductQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductListModel> Handle(GetListProductQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Product> products = await _productRepository.GetListAsync(
            include: p=>
            p.Include(p=>p.Brand)
            .Include(p=>p.Color)
            .Include(p=>p.Category)
            .Include(p=>p.Company),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<ProductListModel>(products);
    }
}
