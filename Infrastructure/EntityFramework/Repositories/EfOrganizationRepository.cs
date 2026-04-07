using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfOrganizationRepository(ContactsDbContext context) : EfGenericRepository<Organization>(context.Organizations), IOrganizationRepositoryAsync
{
    public async Task<IEnumerable<Organization>> FindByTypeAsync(OrganizationType type)
    {
        return await context.Organizations.Where(o => o.Type == type).ToListAsync();
    }

    public async Task<IEnumerable<Person>> GetEmployeesAsync(Guid organizationId)
    {
        return await context.Organizations.Where(o => o.Id == organizationId).SelectMany(o => o.Members).ToListAsync();
    }
}