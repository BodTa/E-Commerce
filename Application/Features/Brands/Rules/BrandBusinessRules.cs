
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandShouldExistWhenRequested(int id)
    {
        Brand? result = await _brandRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Brand is not exists");
    }

    public async Task BrandNameCannotBeDuplicated(string Name)
    {
        var result = await _brandRepository.GetListAsync(b => b.Name == Name);
        if (result.Items.Any()) throw new BusinessException("Brand name cannot be duplicated");
    }
}
