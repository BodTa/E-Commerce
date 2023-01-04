

using Application.Features.Companies.DTOs;
using Core.Persistence.Paging;

namespace Application.Features.Companies.Models;

public class CompanyListModel : BasePageableModel
{
    public IList<CompanyListDto> Items { get; set; }
}
