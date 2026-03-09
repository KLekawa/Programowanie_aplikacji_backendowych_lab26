using AppCore.Models;

namespace AppCore.Interfaces;

public interface ICompanyRepositoryAsync : IGenericRepositoryAsync<Company>
{
    Task<IEnumerable<Company>> SearchByNameAsync(string name);
    Task<Company?> FindByNipAsync(string nip);
    Task<IEnumerable<Person>> GetEmployeesAsync(Guid companyId);
}