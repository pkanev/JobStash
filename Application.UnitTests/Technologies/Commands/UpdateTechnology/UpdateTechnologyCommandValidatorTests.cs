using JobStash.Application.Technologies.Commands.UpdateTechnology;

namespace JobStash.Application.UnitTests.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TechnologyShouldNotHaveNullOrEmptyName(string name)
    {
        var validator = new UpdateTechnologyCommandValidator();
        var request = new UpdateTechnologyCommand
        {
            Id = 1,
            Name = name
        };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(UpdateTechnologyCommand.Name)));
    }
}
