using System.Text.Json;
using System.Text.Json.Serialization;
using AppCore.Interfaces;
using AppCore.Module;
using AppCore.Services;
using FluentValidation.AspNetCore;
using Infrastructure.EntityFramework.Context;
using Infrastructure.Memory;
using Infrastructure.Module;
using Infrastructure.Security;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        
        builder.Services.AddContactsEfModule(builder.Configuration);
        // builder.Services.AddContactsCoreModule(builder.Configuration);

        
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddControllers();
        
        builder.Services.AddSingleton<JwtSettings>();
        builder.Services.AddJwt(new JwtSettings(builder.Configuration));

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();    
        builder.Services.AddProblemDetails();

        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            using var scope = app.Services.CreateScope();

            // Pobieramy wszystkie seedery i sortujemy po Order
            var seeders = scope.ServiceProvider
                .GetServices<IDataSeeder>()
                .OrderBy(s => s.Order);

            foreach (var seeder in seeders)
                await seeder.SeedAsync();
	
        }
        
        app.UseExceptionHandler(); // ta warstwa musi być przed mapowaniem kontrolerów
        app.MapControllers();
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.Run();
    }
}