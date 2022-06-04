using System.Runtime.Serialization;
using AutoMapper;
using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Common.Mappings;
using JobStash.Application.Companies.Queries.GetCompaniesWithPagination;
using JobStash.Application.Companies.Queries.GetCompany;
using JobStash.Application.Technologies.Queries.Dtos;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider configuration;
    private readonly IMapper mapper;

    public MappingTests()
    {
        configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        mapper = configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [InlineData(typeof(Company), typeof(CompanyDto))]
    [InlineData(typeof(Company), typeof(CompanyBriefDto))]
    [InlineData(typeof(Ad), typeof(AdDto))]
    [InlineData(typeof(Ad), typeof(AdBriefDto))]
    [InlineData(typeof(Technology), typeof(TechnologyDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
