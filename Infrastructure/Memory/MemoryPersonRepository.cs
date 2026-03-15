using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonRepository : MemoryGenericRepository<Person>, IPersonRepositoryAsync
{
    public MemoryPersonRepository() : base()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        
        _data.Add(guid1, new Person()
        {
            Id = guid1,
            FirstName = "Adam",
            LastName = "Nowak",
            Gender = Gender.Male
        });
        _data.Add(guid2, new Person()
        {
            Id = guid2,
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