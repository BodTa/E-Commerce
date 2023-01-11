

using Application.Features.Colors.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Queries.GetListColor;

public class GetListColorQuery : IRequest<ColorListModel>
{
    public PageRequest PageRequest { get; set; }
}

public class GetListColorQueryHandler : IRequestHandler<GetListColorQuery, ColorListModel>
{
    private readonly IMapper _mapper;
    private readonly IColorRepository _colorRepository;

    public GetListColorQueryHandler(IMapper mapper, IColorRepository colorRepository)
    {
        _mapper = mapper;
        _colorRepository = colorRepository;
    }

    public async Task<ColorListModel> Handle(GetListColorQuery request, CancellationToken cancellationToken)
    {
        IPaginate < Color > colors = await _colorRepository.GetListAsync(index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<ColorListModel>(colors);
    }
}
