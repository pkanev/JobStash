using AutoMapper;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.UnitTests.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobStash.Application.UnitTests;

internal class Tests
{
    private static IMapper? mapper;

    public static IMapper Mapper
    {
        get
        {
            if (mapper != null)
                return mapper;

            var config = new MapperConfiguration(cfg => {
                cfg.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext)));
            });

            mapper = config.CreateMapper();
            return mapper;
        }
    }

    public static TestDbContext GetContext()
    {
        var dbOptions = new DbContextOptionsBuilder<TestDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new TestDbContext(dbOptions);
    }
}
