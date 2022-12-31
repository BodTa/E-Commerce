

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Color: Entity
{
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public Color()
    {
        Products = new HashSet<Product>();
    }

    public Color(int id, string name):this()
    {
        Name = name;
        Id = id;
    }
}
