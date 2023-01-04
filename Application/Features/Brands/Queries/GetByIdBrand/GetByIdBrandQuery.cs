

using Application.Features.Brands.DTOs;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQuery : IRequest<BrandDto>
{
    public int Id { get; set; }
}

public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandDto>
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly BrandBusinessRules rules;

    public GetByIdBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules rules)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        this.rules = rules;
    }

    public async Task<BrandDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
    {
        await rules.BrandShouldExistWhenRequested(request.Id);
        var brand = await _brandRepository.GetAsync(c => c.Id == request.Id);
        return _mapper.Map<BrandDto>(brand);
    }
}
