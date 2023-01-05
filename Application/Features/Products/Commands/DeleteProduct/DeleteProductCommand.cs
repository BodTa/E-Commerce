

using Application.Features.Products.DTOs;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<DeletedProductDto>
{
    public int Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeletedProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;

    public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<DeletedProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);
        var mappedProduct = _mapper.Map<Product>(request);
        var deletedProduct = await _productRepository.DeleteAsync(mappedProduct);
        return _mapper.Map<DeletedProductDto>(deletedProduct);
    }
}
