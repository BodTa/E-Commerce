


using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetListBrand;

public class GetListBrandQuery : IRequest<BrandListModel>
{
    public PageRequest PageRequest { get; set; }
}

public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;

    public GetListBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }

    public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<BrandListModel>(brands);
    }
}
