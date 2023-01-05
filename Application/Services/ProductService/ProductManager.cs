

using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ProductService;

public class ProductManager : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductManager(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetProductById(int id)
    {
         return await _productRepository.GetAsync(p => p.Id == id,p=>p.Include(p=>p.Company)
                                                                                                                 .Include(p=>p.Brand)
                                                                                                                 .Include(p=>p.Category)
                                                                                                                 .Include(p=>p.Color));
    }
}
