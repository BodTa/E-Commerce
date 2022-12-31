

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Brand : Entity
{
    public string Name { get; set; }
	public virtual ICollection<Product> Products { get; set; }
	public Brand()
	{
		Products = new HashSet<Product>();
	}

	public Brand(int id,string name):this()
	{
		Id = id;
		Name = name;
	}
}
