

using Application.Features.Colors.DTOs;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.UpdateColor;

public class UpdateColorCommand : IRequest<UpdatedColorDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdatedColorCommandHandler : IRequestHandler<UpdateColorCommand, UpdatedColorDto>
{
    private readonly IMapper _mapper;
    private readonly IColorRepository _colorRepository;
    private readonly ColorBusinessRules rules;

    public UpdatedColorCommandHandler(IMapper mapper, IColorRepository colorRepository, ColorBusinessRules rules)
    {
        _mapper = mapper;
        _colorRepository = colorRepository;
        this.rules = rules;
    }

    public async Task<UpdatedColorDto> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
    {
        await rules.ColorShouldExistWhenRequested(request.Id);
        await rules.ColorNameCannotBeDuplicated(request.Name);

        var color = _mapper.Map<Color>(request);
        var updatedColor = await _colorRepository.UpdateAsync(color);
        return _mapper.Map<UpdatedColorDto>(updatedColor);
    }
}
