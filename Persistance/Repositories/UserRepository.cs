

using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class UserRepository :EfRepositoryBase<User,BaseDbContext> ,IUserRepository
{
	public UserRepository(BaseDbContext context) : base(context)
	{

	}
}
