

using Application.Features.Categories.DTOs;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<UpdatedCategoryDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryBusinessRules rules;

    public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, CategoryBusinessRules rules)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        this.rules = rules;
    }

    public async Task<UpdatedCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await rules.CategoryShouldExistWhenRequested(request.Id);
        var mappedCategory = _mapper.Map<Category>(request);
        var updatedCategory = await _categoryRepository.UpdateAsync(mappedCategory);
        return _mapper.Map<UpdatedCategoryDto>(updatedCategory);
    }
}
