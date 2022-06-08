using JobStash.Application.Common.Exceptions;
using JobStash.Application.Technologies.Commands.UpdateTechnology;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task UpdatingNonExistingTechnologyThrowsNotFound()
    {
        var request = new UpdateTechnologyCommand
        {
            Id = 1,
            Name = "tech"
        };

        var handler = new UpdateTechnologyCommandHandler(Context);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task UpdatingCompanyUpdatesEntityInDatabase()
    {
        var technology = new Technology
        {
            Name = "tech"
        };

        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);
        string expectedName = "newTech";
        var request = new UpdateTechnologyCommand
        {
            Id = technology.Id,
            Name = expectedName
        };

        var handler = new UpdateTechnologyCommandHandler(Context);
        await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expectedName, technology.Name);
    }
}
