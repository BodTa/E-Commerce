

using Application.Features.Products.DTOs;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdatedProductDto>
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int ColorId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int Quantity { get; set; }
    public decimal Cost { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;

    public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<UpdatedProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);
        var mappedProduct = _mapper.Map<Product>(request);
        var updatedProduct = await _productRepository.UpdateAsync(mappedProduct);
        return _mapper.Map<UpdatedProductDto>(updatedProduct);
    }
}