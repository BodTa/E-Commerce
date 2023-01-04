

using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Colors.Rules;

public class ColorBusinessRules
{
    private readonly IColorRepository _colorRepository;

    public ColorBusinessRules(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }
    public async Task ColorShouldExistWhenRequested(int id)
    {
        var color = await _colorRepository.GetAsync(c => c.Id == id);
        if (color == null) throw new BusinessException("Color does not exists.");
    }
    
    public async Task ColorNameCannotBeDuplicated(string name)
    {
        var color = await _colorRepository.GetAsync(c => c.Name == name);
        if (color != null) throw new BusinessException("Color name cannot be duplicated");
    }
}
