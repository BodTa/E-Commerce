namespace Application.Features.UserOperationClaims.DTOs;

public class CreatedUserOperationClaimDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}