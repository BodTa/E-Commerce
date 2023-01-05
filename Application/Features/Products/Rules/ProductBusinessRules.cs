
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Products.Rules;

public class ProductBusinessRules 
{
    private readonly IProductRepository _productRepository;

    public ProductBusinessRules(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ProductShouldExistWhenRequested(int id)
    {
        var product = await _productRepository.GetAsync(p => p.Id == id);
        if (product == null) throw new BusinessException("Product does not exists");
    }
    public async Task ACompanyCannotCreateSameNamedProduct(int companyId,string name)
    {
        var product = await _productRepository.GetAsync(p => p.Name == name && p.CompanyId == companyId);
        if (product != null) throw new BusinessException("A Company cannot create same product.");
    }
}
