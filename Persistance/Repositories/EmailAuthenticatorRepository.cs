

using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class EmailAuthenticatorRepository :EfRepositoryBase<EmailAuthenticator,BaseDbContext>, IEmailAuthenticatorRepository
{
	public EmailAuthenticatorRepository(BaseDbContext context) : base(context)
	{

	}
}
