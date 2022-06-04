using AutoMapper;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Companies.Queries.GetCompany;

public record GetCompanyQuery(int Id) : IRequest<CompanyDto>;

public class GetCompanyQueryRequestHandler : IRequestHandler<GetCompanyQuery, CompanyDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetCompanyQueryRequestHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Companies.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Company), request.Id);

        return mapper.Map<CompanyDto>(entity);
    }
}
