

using Application.Features.Products.DTOs;
using Core.Persistence.Paging;

namespace Application.Features.Products.Models;

public class ProductListModel : BasePageableModel
{
    public IList<ProductListDto> Items { get; set; }
}
