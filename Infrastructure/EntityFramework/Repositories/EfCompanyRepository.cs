using System.Globalization;
using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfCompanyRepository(ContactsDbContext context) : EfGenericRepository<Company>(context.Companies), ICompanyRepositoryAsync
{
    public async Task<IEnumerable<Company>> SearchByNameAsync(string name)
    {
        return await context.Companies
            .Where(c => c.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<Company?> FindByNipAsync(string nip)
    {
        return await context.Companies
            .FirstOrDefaultAsync(c => c.NIP == nip);
    }

    public async Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId)
    {
        var result = await context.Companies
            .Where(c => c.Id == companyId)
            .SelectMany(c => c.Employees)
            .ToListAsync();
        return result;

    }
}