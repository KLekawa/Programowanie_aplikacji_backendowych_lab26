using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.Seeders;

public class PersonDbSeeder : IDataSeeder
{
    public int Order => 2;
    
    private readonly ContactsDbContext _contactsDbContext;

    public PersonDbSeeder(ContactsDbContext contactsDbContext)
    {
        _contactsDbContext = contactsDbContext;
    }
    public async Task SeedAsync()
    {
        var people = new[]
        {
            new Person
            {
                Id = Guid.Parse("937348fd-7f13-4b8f-9c28-bb13639cf13c"),
                FirstName = "Bob",
                LastName = "Bobskowski",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = Gender.Male,
                Position = "Sprzedawca",
                Email = "bob.bobskowski@mail.com",
                Phone = "486479248",
                CreatedAt = DateTime.Now,
                Status = ContactStatus.Active
            },
            new Person
            {
                Id = Guid.Parse("3f1bab9c-dc9c-475b-b281-755e3a23b150"),
                FirstName = "Leon",
                LastName = "Leonowski",
                BirthDate = new DateTime(1985, 11, 16),
                Gender = Gender.Male,
                Position = "Menadżer",
                Email = "leon.leonowski@mail.com",
                Phone = "278369574",
                CreatedAt = DateTime.Now,
                Status = ContactStatus.Active
            }
        };

        foreach (var person in people)
        {
            if (await _contactsDbContext.People.FindAsync(person.Id) is not null)
                continue;
            
            await _contactsDbContext.People.AddAsync(person);
        }
            await _contactsDbContext.SaveChangesAsync();
    }
}