using JobStash.Application.Common.Interfaces;
using JobStash.Infrastructure.Persistence;
using JobStash.Infrastructure.Persistence.Interceptors;
using JobStash.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("JobStashDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options => {
                options.Authority = configuration["Authority"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "JobStashApplication");
            });
        });

        services.AddTransient<IDateTime, DateTimeService>();
        //services.AddTransient<IIdentityService, IdentityService>();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
