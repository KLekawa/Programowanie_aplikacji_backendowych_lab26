using AppCore.Interfaces;
using AppCore.Services;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Module;

public static class ContactsInfrastructureModule
{
    public static IServiceCollection AddContactsEfModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ICompanyRepositoryAsync, EfCompanyRepository>();
        services.AddScoped<IPersonRepositoryAsync, EfPersonRepository>();
        services.AddScoped<IContactRepositoryAsync, EfContactRepository>();
        services.AddScoped<IOrganizationRepositoryAsync, EfOrganizationRepository>();
        services.AddScoped<IContactUntiOfWork, EfContactsUnitOfWork>();
        services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("CrmDb")));
        
        services.AddIdentity<CrmUser, CrmRole>(options =>
            {
                options.Password.RequiredLength         = 8;
                options.Password.RequireUppercase       = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail         = true;
                options.SignIn.RequireConfirmedEmail    = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<ContactsDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<IPersonService, PersonService>();
        return services;
    }
}