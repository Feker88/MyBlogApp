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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CodePulse.Application.Services;

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

        // ─── Data Protection (required by Identity token providers) ───
        services.AddDataProtection();

        // ─── identity ───
        services.AddIdentityCore<IdentityUser>(options =>
        {
        // ─── Identity password options ───
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDBContext>()
            .AddDefaultTokenProviders();


        // ─── JWT Authentication ───
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the server that created the token
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],

                    // Validate the recipient of the token
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // Validate the secret signing key
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

        // ─── Authorization ───
        services.AddAuthorization();

        // ─── Repositories ───
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // ─── UnitOfWork ───
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();


        // ─── Auth service───
        services.AddScoped<IAuthServices,AuthService>();

        return services;
    }
}