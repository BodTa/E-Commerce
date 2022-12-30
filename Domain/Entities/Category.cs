

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Category:Entity
{
    public string Name { get; set; }

	public string Description { get; set; }
	public Category()
	{

	}

	public Category(int id ,string name, string description):base(id)
	{
		Name = name;
		Description = description;
	}
}
