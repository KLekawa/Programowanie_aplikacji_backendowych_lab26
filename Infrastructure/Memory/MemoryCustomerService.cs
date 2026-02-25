using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryCustomerService : ICustomerService
{
    public IEnumerable<Customer> GetCustomer()
    {
        return
        [
            new Customer()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@email.com",
                Phone = "111-222-333",
                AddressId = 11
            },
            new Customer()
            {
                Id = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna.nowak@email.com",
                Phone = "444-555-666",
                AddressId = 22
            }
        ];
    }

    public Task<IEnumerable<Customer>> GetCustomerAsync()
    {
        throw new NotImplementedException();
    }
}