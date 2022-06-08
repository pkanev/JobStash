using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Common.Mappings;
using JobStash.Application.Common.Models;
using JobStash.Application.Technologies.Queries.Dtos;
using JobStash.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Technologies.Queries.GetTechnologiesForAdWithPagination;

public record GetTechnologiesForAdWithPaginationQuery : IRequest<PaginatedList<TechnologyDto>>
{
    public int AdId { get; init; }
    public int TechnologyId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTechnologiesForAdWithPaginationQueryHandler : IRequestHandler<GetTechnologiesForAdWithPaginationQuery, PaginatedList<TechnologyDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetTechnologiesForAdWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<TechnologyDto>> Handle(
        GetTechnologiesForAdWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var ad = await context.Ads
            .FirstOrDefaultAsync(a => a.Id == request.AdId, cancellationToken);

        if (ad == null)
            throw new NotFoundException(nameof(Ad), request.AdId);
        
        return await context.Technologies
            .Where(t => t.Ads.Any(a => a.Id == request.AdId))
            .OrderBy(t => t.Name)
            .ProjectTo<TechnologyDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}

