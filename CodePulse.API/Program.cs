using AutoMapper;
using CodePulse.Application;
using CodePulse.Application.Mappers;
using CodePulse.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json.Serialization;

namespace CodePulse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ─── Kestrel HTTPS Configuration ───
            //builder.WebHost.ConfigureKestrel(options =>
            //{
            //    // HTTP endpoint
            //    options.ListenLocalhost(5241);

            //    // HTTPS endpoint — uses the dev certificate automatically
            //    options.ListenLocalhost(7241, listenOptions =>
            //    {
            //        listenOptions.UseHttps();
            //    });
            //});

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Map controller services 
            builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer();

            // ─── Clean single-line registrations ───
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            
            var app = builder.Build();

            // ─── Validate AutoMapper configuration at startup ───
            if (app.Environment.IsDevelopment())
            {
                var mapper = app.Services.GetRequiredService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    
    }
}
