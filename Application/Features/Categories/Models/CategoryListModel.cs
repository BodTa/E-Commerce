
using Application.Features.Categories.DTOs;
using Core.Persistence.Paging;

namespace Application.Features.Categories.Models;

public class CategoryListModel : BasePageableModel
{
    public IList<CategoryListDto> Items { get; set; }
}
