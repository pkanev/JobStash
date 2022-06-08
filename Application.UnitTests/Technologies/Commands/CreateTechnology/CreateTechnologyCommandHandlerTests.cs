using JobStash.Application.Common.Exceptions;
using JobStash.Application.Technologies.Commands.CreateTechnology;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task AddingATechnologyShouldReturnTechnologyId()
    {
        var request = new CreateTechnologyCommand("Test");

        var handler = new CreateTechnologyCommandHandler(Context);
        var result = await handler.Handle(request, CancellationToken.None);
        var technology = Context.Technologies.Find(result);

        Assert.IsType<int>(result);
        Assert.NotNull(technology);
        Assert.Equal(technology?.Id, result);
    }

    [Theory]
    [InlineData("tech")]
    [InlineData("   tech")]
    [InlineData("tech   ")]
    [InlineData(" tech   ")]
    [InlineData("  tEcH  ")]
    public async Task AddingADuplicateTechnologyThrowsDuplicatingTechnology(string name)
    {
        Context.Technologies.Add(new Technology { Name = name.Trim() });
        await Context.SaveChangesAsync(CancellationToken.None);
        var request = new CreateTechnologyCommand(name);

        var handler = new CreateTechnologyCommandHandler(Context);
        await Assert.ThrowsAnyAsync<DuplicatingTechnologyExcpetion>(() => handler.Handle(request, CancellationToken.None));
    }
}
