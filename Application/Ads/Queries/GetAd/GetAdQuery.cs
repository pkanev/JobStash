using AutoMapper;
using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Ads.Queries.GetAd;

public record GetAdQuery(int Id) : IRequest<AdDto>;

public class GetAdQueryHandler : IRequestHandler<GetAdQuery, AdDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<AdDto> Handle(GetAdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Ads
            .Include(a => a.Company)
            .Include(a => a.Technologies)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Ad), request.Id);

        return mapper.Map<AdDto>(entity);
    }
}
