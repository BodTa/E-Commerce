

using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class ColorRepository :EfRepositoryBase<Color,BaseDbContext> ,IColorRepository
{
	public ColorRepository(BaseDbContext context) : base(context)
	{

	}
}
