

using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class BrandRepository :EfRepositoryBase<Brand,BaseDbContext>  ,IBrandRepository
{
	public BrandRepository(BaseDbContext context) : base(context)
	{

	}
}
