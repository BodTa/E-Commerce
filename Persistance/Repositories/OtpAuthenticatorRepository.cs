

using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class OtpAuthenticatorRepository :EfRepositoryBase<OtpAuthenticator,BaseDbContext> ,IOtpAuthenticatorRepository
{
	public OtpAuthenticatorRepository(BaseDbContext context) : base(context)
	{

	}
}
