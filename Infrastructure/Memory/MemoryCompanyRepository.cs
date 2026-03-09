using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryCompanyRepository : MemoryGenericRepository<Company>, ICompanyRepositoryAsync
{
    public Task<IEnumerable<Company>> SearchByNameAsync(string name)
    {
        var result = _data.Values.Where(x => x.Name.ToLower().Contains(name.ToLower()));
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<Company?> FindByNipAsync(string nip)
    {
        var result = _data.Values.FirstOrDefault(n => n.NIP == nip);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId)
    {
        var result = _data.Values
            .Where(c => c.Id == companyId)
            .SelectMany(c => c.Employees)
            .AsEnumerable();

        return Task.FromResult(result);
    }
}