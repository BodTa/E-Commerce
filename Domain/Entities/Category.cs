

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Category:Entity
{
    public string Name { get; set; }


	public virtual ICollection<Brand> Brands { get; set; }
	public Category()
	{
		Brands = new HashSet<Brand>();
	}

	public Category(int id ,string name):base(id)
	{
		Name = name;
	}
}
