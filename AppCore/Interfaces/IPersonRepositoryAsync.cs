using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonRepositoryAsync : IGenericRepositoryAsync<Person>
{
    Task<IEnumerable<Person>> FindByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<Person>> FindByOrganizationIdAsync(Guid organizationId);
}