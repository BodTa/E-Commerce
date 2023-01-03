
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class CategoryRepository : EfRepositoryBase<Category,BaseDbContext>,ICategoryRepository
{
	public CategoryRepository(BaseDbContext context) : base(context)
	{

	}
}
