

using Application.Features.Colors.DTOs;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Colors.Queries.GetByIdColor;

public class GetByIdColorQuery : IRequest<ColorDto>
{
    public int Id { get; set; }
}

public class GetByIdColorQueryHandler : IRequestHandler<GetByIdColorQuery, ColorDto>
{
    private readonly IMapper _mapper;
    private readonly IColorRepository  _colorRepository;
    private readonly ColorBusinessRules rules;

    public GetByIdColorQueryHandler(IMapper mapper, IColorRepository colorRepository, ColorBusinessRules rules)
    {
        _mapper = mapper;
        _colorRepository = colorRepository;
        this.rules = rules;
    }

    public async Task<ColorDto> Handle(GetByIdColorQuery request, CancellationToken cancellationToken)
    {
        await rules.ColorShouldExistWhenRequested(request.Id);
        var color = await _colorRepository.GetAsync(c => c.Id == request.Id);
        return _mapper.Map<ColorDto>(color);
    }
}
