using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;

namespace JobStash.Application.Technologies.Commands.AddTechnologyToAd;

public record AddTechnologyToAdCommand(int AdId, int TechnologyId) : IRequest;

public class AddTechnologyToAdCommandHandler : IRequestHandler<AddTechnologyToAdCommand, Unit>
{
    private readonly IApplicationDbContext context;

    public AddTechnologyToAdCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(AddTechnologyToAdCommand request, CancellationToken cancellationToken)
    {
        var ad = await context.Ads.FindAsync(new object[] { request.AdId }, cancellationToken);
        if (ad == null)
            throw new NotFoundException(nameof(Ad), request.AdId);

        var technology = await context.Technologies.FindAsync(new object[] { request.TechnologyId }, cancellationToken);
        if (technology == null)
            throw new NotFoundException(nameof(Technology), request.TechnologyId);

        ad.Technologies.Add(technology);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
