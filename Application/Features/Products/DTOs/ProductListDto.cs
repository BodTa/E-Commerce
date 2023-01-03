
namespace Application.Features.Products.DTOs;

public class ProductListDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
    public string ColorName { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedDate { get; set; }
}
