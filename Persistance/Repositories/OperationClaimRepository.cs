
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class OperationClaimRepository :EfRepositoryBase<OperationClaim,BaseDbContext> ,IOperationClaimRepository
{
	public OperationClaimRepository(BaseDbContext context) : base(context)
	{

	}
}
