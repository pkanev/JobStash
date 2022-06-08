using JobStash.Application.Technologies.Commands.CreateTechnology;

namespace JobStash.Application.UnitTests.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void NewTechnologyShouldNotHaveNullName(string? name)
    {
        var validator = new CreateTechnologyCommandValidator();
        var request = new CreateTechnologyCommand(name);
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(CreateTechnologyCommand.Name)));
    }
}
