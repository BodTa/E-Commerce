

using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Models;

public class OperationClaimListModel : BasePageableModel
{
    public IList<OperationClaim> Items { get; set; }
}
