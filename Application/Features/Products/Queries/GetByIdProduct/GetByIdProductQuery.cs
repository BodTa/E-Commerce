

using Application.Features.Products.DTOs;
using Application.Features.Products.Rules;
using Application.Services.ProductService;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetByIdProduct;

public class GetByIdProductQuery : IRequest<ProductDto>
{
    public int Id { get; set; }
}

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly ProductBusinessRules _productBusinessRules;

    public GetByIdProductQueryHandler(IMapper mapper, IProductService productService, ProductBusinessRules productBusinessRules)
    {
        _mapper = mapper;
        _productService = productService;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<ProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);
        var product = await _productService.GetProductById(request.Id);
        return _mapper.Map<ProductDto>(product);
    }
}
