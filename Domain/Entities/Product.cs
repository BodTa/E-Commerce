
using Core.Persistence.Repositories;
using System.Drawing;

namespace Domain.Entities;

public class Product :Entity
{
    public int Name { get; set; }

    public int BrandId { get; set; }

    public int? ColorId { get; set; }
    public int CategoryId { get; set; }

    public string Description { get; set; }

    public virtual Color? Color { get; set; }
    public virtual Brand
    public DateTime CreatedDate { get; set; }

    public Product()
    {

    }

  

}
