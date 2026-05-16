using CodePulse.Application.Interfaces;
using CodePulse.Application.Mappers;
using CodePulse.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodePulse.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // ─── AutoMapper ───
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(BlogPostMappingProfile).Assembly);
        });

        // ─── Services ───
        services.AddScoped<IBlogPostServices, BlogPostServices>();
        services.AddScoped<ICategoryPostService, CategoryPostService>();

        return services;
    }
}