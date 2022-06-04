using AutoMapper;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Technologies.Queries.Dtos;
using JobStash.Domain.Entities;
using MediatR;

namespace JobStash.Application.Technologies.Queries.GetTechnology;

public record GetTechnologyQuery(int Id) : IRequest<TechnologyDto>;

public class GetTechnologyQueryRequesthandler : IRequestHandler<GetTechnologyQuery, TechnologyDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetTechnologyQueryRequesthandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<TechnologyDto> Handle(GetTechnologyQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Technologies.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Technology), request.Id);

        return mapper.Map<TechnologyDto>(entity);
    }
}
