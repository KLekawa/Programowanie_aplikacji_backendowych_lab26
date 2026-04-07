using AppCore.Interfaces;
using AppCore.Module;
using AppCore.Services;
using FluentValidation.AspNetCore;
using Infrastructure.EntityFramework.Context;
using Infrastructure.Memory;
using Infrastructure.Module;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddContactsEfModule(builder.Configuration);
        // builder.Services.AddContactsCoreModule(builder.Configuration);

        
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();    
        builder.Services.AddProblemDetails();

        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.UseExceptionHandler(); // ta warstwa musi być przed mapowaniem kontrolerów
        app.MapControllers();
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.Run();
    }
}