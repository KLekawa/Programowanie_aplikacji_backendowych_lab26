using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonRepository : MemoryGenericRepository<Person>, IPersonRepositoryAsync
{
    public Task<IEnumerable<Person>> FindByCompanyIdAsync(Guid companyId)
    {
        var result = _data.Values.Where(p => p.Employer.Id == companyId);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Person>> FindByOrganizationIdAsync(Guid organizationId)
    {
        var result = _data.Values.Where(p => p.Organization.Id == organizationId);
        return Task.FromResult(result);
    }
}