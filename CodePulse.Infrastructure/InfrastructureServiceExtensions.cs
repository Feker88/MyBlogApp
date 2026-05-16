using CodePulse.Application.Interfaces;
using CodePulse.Domain.Repositories;
using CodePulse.Infrastructure.Data;
using CodePulse.Infrastructure.Repositories;
using CodePulse.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace CodePulse.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ─── DbContext ───
        services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CodePulseDb")));
        
        // ─── identity ───

        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CodePulse")
            .AddEntityFrameworkStores<AppDBContext>()
            .AddDefaultTokenProviders();

        // ─── Repositories ───
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // ─── UnitOfWork ───
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}