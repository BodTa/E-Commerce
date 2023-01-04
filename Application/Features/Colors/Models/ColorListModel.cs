
using Application.Features.Colors.DTOs;
using Core.Persistence.Paging;

namespace Application.Features.Colors.Models;

public class ColorListModel : BasePageableModel
{
    public IList<ColorListDto> Items { get; set; }
}
