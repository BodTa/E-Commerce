

using Application.Features.Categories.DTOs;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<DeletedCategoryDto>
{
    public int Id { get; set; }
}

public class   DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeletedCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryBusinessRules rules;

    public DeleteCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, CategoryBusinessRules rules)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        this.rules = rules;
    }

    public async Task<DeletedCategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await rules.CategoryShouldExistWhenRequested(request.Id);
        var mappedCategory = _mapper.Map<Category>(request);
        var deletedCategory = await _categoryRepository.DeleteAsync(mappedCategory);
        return _mapper.Map<DeletedCategoryDto>(deletedCategory);
    }
}
