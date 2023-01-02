
using Core.Persistence.Repositories;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities;

public class Product :Entity
{
    public int BrandId { get; set; }

    public int ColorId { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public int Quantity { get; set; }
    public decimal Cost { get; set; }

    public virtual Color? Color { get; set; }
    public virtual Brand? Brand { get; set; }
    public virtual Category? Category { get; set; }
    public virtual Company? Company { get; set; }   
    public DateTime CreatedDate { get; set; }

    public Product()
    {

    }

    public Product(int id, int brandId, int colorId,int companyId, int categoryId, int quantity, decimal cost, string name, string description, DateTime createdDate): this()
    {
        Id = id;
        
        BrandId = brandId;
        ColorId = colorId;
        CompanyId = companyId;
        CategoryId = categoryId;
        Quantity =quantity;
        Cost =cost;
        Name = name;
        Description = description;
        CreatedDate = createdDate;
    }
}
