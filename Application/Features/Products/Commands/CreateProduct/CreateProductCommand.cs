
using Application.Features.Products.DTOs;
using Application.Services.Repositories;
using AutoMapper;
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

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        

        throw new NotImplementedException();
    }
}
