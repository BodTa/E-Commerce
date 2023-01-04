

using Application.Features.Categories.DTOs;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CreatedCategoryDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, CreatedCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryBusinessRules rules;

    public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, CategoryBusinessRules rules)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        this.rules = rules;
    }

    public async Task<CreatedCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await rules.CategoryNameCannotBeDuplicated(request.Name);
        var mappedCategory = _mapper.Map<Category>(request);
        var createdCategory = await _categoryRepository.AddAsync(mappedCategory);
        return _mapper.Map<CreatedCategoryDto>(createdCategory);
    }
}
