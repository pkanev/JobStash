using AutoMapper;
using JobStash.Application.Common.Interfaces;

namespace JobStash.Application.UnitTests.Context;

public abstract class ContextBaseTests
{
    private IApplicationDbContext context;
    private IMapper mapper;

    protected IApplicationDbContext Context => context;
    protected IMapper Mapper => mapper;

    protected ContextBaseTests()
    {
        context = Tests.GetContext();
        mapper = Tests.Mapper;
    }
}
