using AppCore.Interfaces;
using AppCore.Module;
using FluentValidation.AspNetCore;
using Infrastructure.Memory;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IPersonRepositoryAsync, MemoryPersonRepository>();
        builder.Services.AddSingleton<ICompanyRepositoryAsync, MemoryCompanyRepository>();
        builder.Services.AddSingleton<IOrganizationRepositoryAsync, MemoryOrganizationRepository>();

        builder.Services.AddSingleton<IContactUntiOfWork, MemoryContactUnitOfWork>();
        
        builder.Services.AddSingleton<IPersonService, MemoryPersonService>();
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}