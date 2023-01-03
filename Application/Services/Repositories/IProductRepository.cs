

using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IProductRepository : IAsyncRepository<Product>, IRepository<Product>
{
}
