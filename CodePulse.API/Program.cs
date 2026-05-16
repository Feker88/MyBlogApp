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
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Map controller services 
            builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer();

            // ─── Clean single-line registrations ───
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            // Register AutoMapper — scans your Application project for all profiles
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(BlogPostMappingProfile).Assembly));

   
            var app = builder.Build();

            // ─── Validate AutoMapper configuration at startup ───
            if (app.Environment.IsDevelopment())
            {
                var mapper = app.Services.GetRequiredService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
                app.MapOpenApi();
            }


            // app.UseHttpsRedirection();            
            // app.UseAuthorization();
            ////app.UseAuthentication();
            app.MapControllers();


            //Todo[] sampleTodos =
            //[
            //    new(1, "Walk the dog"),
            //    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
            //    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
            //    new(4, "Clean the bathroom"),
            //    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
            //];

            //var todosApi = app.MapGroup("/todos");
            //todosApi.MapGet("/", () => sampleTodos)
            //        .WithName("GetTodos");

            //todosApi.MapGet("/{id}", Results<Ok<Todo>, NotFound> (int id) =>
            //    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
            //        ? TypedResults.Ok(todo)
            //        : TypedResults.NotFound())
            //    .WithName("GetTodoById");

            app.Run();
        }
    }

    public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

    [JsonSerializable(typeof(Todo[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
