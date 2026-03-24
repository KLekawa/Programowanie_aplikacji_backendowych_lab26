using AppCore.Validators;
using AppCore.Validators.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppCore.Module;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Rejestracja walidatorów
        services.AddValidatorsFromAssemblyContaining<CreatePersonDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdatePersonDtoValidator>();
        services.AddFluentValidationAutoValidation();
        return services;
    }
}