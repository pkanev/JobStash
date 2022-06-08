using JobStash.Application.Common.Exceptions;
using JobStash.Application.Technologies.Queries.Dtos;
using JobStash.Application.Technologies.Queries.GetTechnology;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Queries.GetTechnology;

public class GetTechnologyQueryTests : ContextBaseTests
{
    [Fact]
    public async Task GetgNonExistentTechnologyThrowsNotFound()
    {
        var random = new Random();
        var request = new GetTechnologyQuery(random.Next(1, 100));

        var handler = new GetTechnologyQueryHandler(Context, Mapper);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task GetCompanyReturnsTechnologyDto()
    {
        var technology = new Technology { Name = "tech" };
        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetTechnologyQueryHandler(Context, Mapper);
        var result = await handler.Handle(new GetTechnologyQuery(technology.Id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<TechnologyDto>(result);
        Assert.Equal(technology.Id, result.Id);
        Assert.Equal(technology.Name, result.Name);
    }
}
