

using Application.Features.Brands.DTOs;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest<DeletedBrandDto>
{
    public int Id { get; set; }

}
public class DeletedBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandDto>
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;
    private readonly BrandBusinessRules rules;

    public DeletedBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules rules)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        this.rules = rules;
    }

    public async Task<DeletedBrandDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        await rules.BrandShouldExistWhenRequested(request.Id);
        var mappedBrand = _mapper.Map<Brand>(request);
        var deletedBrand = await _brandRepository.DeleteAsync(mappedBrand);
        return _mapper.Map<DeletedBrandDto>(deletedBrand);
    }
}
