using AppCore.Interfaces;
using AppCore.Services;
using Infrastructure.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Module;

public static class AddContactsMemoryModule
{
    public static IServiceCollection AddContactsCoreModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPersonRepositoryAsync, MemoryPersonRepository>();
        services.AddScoped<ICompanyRepositoryAsync, MemoryCompanyRepository>();
        services.AddScoped<IOrganizationRepositoryAsync, MemoryOrganizationRepository>();
        services.AddScoped<IContactUntiOfWork, MemoryContactUnitOfWork>();
        // services.AddScoped<IPersonRepositoryAsync, PersonService>();
        

        
        return services;
    }
}