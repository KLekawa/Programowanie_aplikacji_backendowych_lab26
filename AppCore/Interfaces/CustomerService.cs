using AppCore.Models;

namespace AppCore.Interfaces;

public interface ICustomerService
{
    public IEnumerable<Customer> GetCustomer();
    public Task<IEnumerable<Customer>> GetCustomerAsync();
}