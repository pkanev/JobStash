
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobStash.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> logger;
    private readonly ApplicationDbContext context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (context.Database.IsSqlServer())
                await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync() => await Task.FromResult(() => { });
}
