using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonRepository : MemoryGenericRepository<Person>, IPersonRepositoryAsync
{
    public MemoryPersonRepository() : base()
    {
        _data.Add(Guid.NewGuid(), new Person()
        {
            FirstName = "Adam",
            LastName = "Nowak",
            Gender = Gender.Male
        });
        _data.Add(Guid.NewGuid(), new Person()
        {
            FirstName = "Ewa",
            LastName = "Kowalska",
            Gender = Gender.Female
        });
    }
    public Task<IEnumerable<Person>> FindByCompanyIdAsync(Guid companyId)
    {
        var result = _data.Values.Where(p => p.Employer is not null && p.Employer.Id == companyId);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Person>> FindByOrganizationIdAsync(Guid organizationId)
    {
        var result = _data.Values.Where(p => p.Organization is not null && p.Organization.Id == organizationId);
        return Task.FromResult(result);
    }
}