using AppCore.Validators;
using AppCore.Validators.Interaction;
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
        services.AddValidatorsFromAssemblyContaining<CreateEmailDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateSmsDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateMeetingDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateInteractionDtoValidator>();
        services.AddFluentValidationAutoValidation();
        return services;
    }
}