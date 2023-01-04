

using Application.Features.Categories.Models;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetListCategor;

public class GetListCategoryQuery : IRequest<CategoryListModel>
{
    public PageRequest  PageRequest{ get; set; }
}

public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, CategoryListModel>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetListCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryListModel> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Category> categories = await _categoryRepository.GetListAsync(index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<CategoryListModel>(categories);
    }
}