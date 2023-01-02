
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Company : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }
    public DateTime JoinDate { get; set; }
    
    public CompanyState State { get; set; }

    public City City { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public Company()
    {
        State = CompanyState.NoPunishment;
        Products = new HashSet<Product>();
    }

    public Company(int id, string name, string description, DateTime joinDate, City city ) : this()
    {
        Id = id;
        Name = name;
        Description = description;
        JoinDate = joinDate;
        City = city;
    }
}
