

using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim;

public class GetListOperationClaimQuery : IRequest<OperationClaimListModel>
{
    public PageRequest PageRequest{ get; set; }
}

public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;

    public GetListOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
    {
        IPaginate < OperationClaim >  claims= await _operationClaimRepository.GetListAsync(index: request.PageRequest.Page,
            size: request.PageRequest.PageSize);
        return _mapper.Map<OperationClaimListModel>(claims);
    }
}
