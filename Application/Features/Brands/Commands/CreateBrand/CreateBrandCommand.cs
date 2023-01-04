

using Application.Features.Brands.DTOs;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<CreatedBrandDto>
{
    public string Name { get; set; }
}

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly BrandBusinessRules rules;

    public CreateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules rules)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        this.rules = rules;
    }

    public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await rules.BrandNameCannotBeDuplicated(request.Name);
        var mappedBrand = _mapper.Map<Brand>(request);
        var createdBrand = await _brandRepository.AddAsync(mappedBrand);
        return _mapper.Map<CreatedBrandDto>(createdBrand);
    }
}
