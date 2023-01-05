

using Domain.Entities;

namespace Application.Services.ProductService;

public interface IProductService
{
    public Task<Product> GetProductById(int id);
}
