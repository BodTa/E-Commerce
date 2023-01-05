
using Application.Features.Products.DTOs;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<CreatedProductDto>
{
    public int BrandId { get; set; }

    public int ColorId { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedDate { get; set; }
}
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _productBusinessRules.ACompanyCannotCreateSameNamedProduct(request.CompanyId, request.Name);
        var mappedProduct = _mapper.Map<Product>(request);
        var createdProduct = await _productRepository.AddAsync(mappedProduct);
        return _mapper.Map<CreatedProductDto>(createdProduct);
    }
}
