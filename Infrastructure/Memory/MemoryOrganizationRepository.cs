using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryOrganizationRepository : MemoryGenericRepository<Organization>, IOrganizationRepositoryAsync
{
    public Task<IEnumerable<Organization>> FindByTypeAsync(OrganizationType type)
    {
        var result = _data.Values.AsEnumerable().Where(c => c.Type == type);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Person>> GetEmployeesAsync(Guid organizationId)
    {
        var result = _data.Values.Where(o => o.Id == organizationId).SelectMany(p => p.Members);
        return Task.FromResult(result);
    }
}