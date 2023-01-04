

using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Categories.Rules;

public class CategoryBusinessRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryBusinessRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task CategoryShouldExistWhenRequested(int id)
    {
        var category = await _categoryRepository.GetAsync(c => c.Id == id);
        if (category == null) throw new BusinessException("Category does not exists");
    }

    public async Task CategoryNameCannotBeDuplicated(string Name)
    {
        var category =await _categoryRepository.GetAsync(c => c.Name == Name);
        if (category != null) throw new BusinessException("Category name cannot be duplicated");
    }
}
