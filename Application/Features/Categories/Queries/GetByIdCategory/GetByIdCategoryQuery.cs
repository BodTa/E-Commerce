

using Application.Features.Categories.DTOs;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries.GetByIdCategory;

public class GetByIdCategoryQuery : IRequest<CategoryDto>
{
    public int Id { get; set; }
}

public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryDto>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryBusinessRules rules;

    public GetByIdCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository, CategoryBusinessRules rules)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        this.rules = rules;
    }

    public async Task<CategoryDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
    {
        await rules.CategoryShouldExistWhenRequested(request.Id);
        var category = await _categoryRepository.GetAsync(c => c.Id == request.Id);
        return _mapper.Map<CategoryDto>(category);
    }
}
