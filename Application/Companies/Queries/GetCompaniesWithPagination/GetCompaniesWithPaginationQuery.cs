using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Common.Mappings;
using JobStash.Application.Common.Models;
using MediatR;

namespace JobStash.Application.Companies.Queries.GetCompaniesWithPagination;

public record GetCompaniesWithPaginationQuery : IRequest<PaginatedList<CompanyBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCompaniesWithPaginationQueryHandler : IRequestHandler<GetCompaniesWithPaginationQuery, PaginatedList<CompanyBriefDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetCompaniesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<CompanyBriefDto>> Handle(
        GetCompaniesWithPaginationQuery request,
        CancellationToken cancellationToken) => await context.Companies
            .OrderBy(c => c.Name)
            .ProjectTo<CompanyBriefDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
}