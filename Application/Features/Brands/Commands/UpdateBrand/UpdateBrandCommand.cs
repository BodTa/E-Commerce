

using Application.Features.Brands.DTOs;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest<UpdatedBrandDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

}

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdatedBrandDto>
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
    }

    public async Task<UpdatedBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var mappedBrand = _mapper.Map<Brand>(request);
        var updatedBrand = await _brandRepository.UpdateAsync(mappedBrand);
        return _mapper.Map<UpdatedBrandDto>(updatedBrand);
    }
}