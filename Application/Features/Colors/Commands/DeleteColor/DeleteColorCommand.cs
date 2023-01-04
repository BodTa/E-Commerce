

using Application.Features.Colors.DTOs;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.DeleteColor;

public class DeleteColorCommand : IRequest<DeletedColorDto>
{
    public int Id { get; set; }
}
public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, DeletedColorDto>
{
    private readonly IMapper _mapper;
    private readonly IColorRepository _colorRepository;
    private readonly ColorBusinessRules rules;

    public DeleteColorCommandHandler(IMapper mapper, IColorRepository colorRepository, ColorBusinessRules rules)
    {
        _mapper = mapper;
        _colorRepository = colorRepository;
        this.rules = rules;
    }

    public async Task<DeletedColorDto> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
    {
        await rules.ColorShouldExistWhenRequested(request.Id);
        var color = _mapper.Map<Color>(request);
        var deletedColor = await _colorRepository.DeleteAsync(color);
        return _mapper.Map<DeletedColorDto>(deletedColor);
    }
}
