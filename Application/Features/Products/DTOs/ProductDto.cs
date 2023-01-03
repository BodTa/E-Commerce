

namespace Application.Features.Products.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }

    public DateTime CreatedDate { get; set; }
}
