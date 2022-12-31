

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Order : Entity
{
    public int UserId { get; set; }
    public string UserLocation { get; set; }

    public virtual ICollection<Product> OrderedProducts { get; set; }




}
