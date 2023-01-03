


using Domain.Enums;

namespace Application.Features.Companies.DTOs;

public class CompanyListDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public DateTime JoinDate { get; set; }

    public CompanyState State { get; set; }

    public City City { get; set; }

}
