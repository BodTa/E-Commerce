

using Application.Features.Colors.DTOs;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.CreateColor;

public class CreateColorCommand : IRequest<CreatedColorDto>
{
    public string Name { get; set; }
}
public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, CreatedColorDto>
{
    private readonly IMapper _mapper;
    private readonly IColorRepository _colorRepsitory;
    private readonly ColorBusinessRules rules;

    public CreateColorCommandHandler(IMapper mapper, IColorRepository colorRepsitory, ColorBusinessRules rules)
    {
        _mapper = mapper;
        _colorRepsitory = colorRepsitory;
        this.rules = rules;
    }

    public async Task<CreatedColorDto> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        await rules.ColorNameCannotBeDuplicated(request.Name);
        var mappedColor = _mapper.Map<Color>(request);
        var createdColor = await _colorRepsitory.AddAsync(mappedColor);
        return _mapper.Map<CreatedColorDto>(createdColor);
    }
}
