using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.Memory;

namespace UnitTest;

public class MemoryGenericRepositoryTest
{
    private IGenericRepositoryAsync<Person> _repo = new MemoryGenericRepository<Person>();

    [Fact]
    public async Task AddPersonTestAsync()
    {
        // Arrange
        var expected = new Person()
        {
            FirstName = "Adam"
        };

        // Act
        await _repo.AddAsync(expected);

        // Assert
        var actual = await _repo.FindByIdAsync(expected.Id);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Id, actual?.Id);
    }

    [Fact]
    public async Task UpdatePersonTestAsync()
    {

        var init = new Person()
        {
            FirstName = "Michał"
        };
        
        await _repo.AddAsync(init);
        init.FirstName = "Stanisław";
        await  _repo.UpdateAsync(init);
        var actual = await _repo.FindByIdAsync(init.Id);
        Assert.Equal(init, actual);
        Assert.Equal(init.Id, init.Id);
    }

    [Fact]
    public async Task FindAllTestAsync()
    {
        var person1 = new Person()
        {
            FirstName = "Adam"
        };
        var person2 = new Person()
        {
            FirstName = "Stanisław"
        };
        await _repo.AddAsync(person1);
        await _repo.AddAsync(person2);
        var actual = await _repo.FindAllAsync();

        Assert.Equal(2, actual.Count());
        Assert.Contains(actual, p => p.Id == person1.Id);
        Assert.Contains(actual, p => p.Id == person2.Id);
    }

    [Fact]
    public async Task RemoveByIdTestAsync()
    {
        var person = new Person(){ FirstName = "Adam" };
    
        await  _repo.AddAsync(person);
        await _repo.RemoveByIdAsync(person.Id);
        var actual = await _repo.FindAllAsync();
        Assert.Empty(actual);
    }

    [Fact]
    public async Task FindPagedTestAsync()
    {
        var person1 = new Person(){FirstName = "Adam"};
        var person2 = new Person(){FirstName = "Ewa"};
        var person3 = new Person(){FirstName = "Staś"};
        var person4 = new Person(){FirstName = "Daniel"};
        var person5 = new Person(){FirstName = "Michał"};
        
        await _repo.AddAsync(person1);
        await _repo.AddAsync(person2);
        await _repo.AddAsync(person3);
        await _repo.AddAsync(person4);
        await _repo.AddAsync(person5);

        var actual = await _repo.FindPagedAsync(1, 2);
        Assert.Equal(actual.TotalCount, 5);
        Assert.Equal(actual.TotalPages, 3);
        Assert.Equal(actual.PageSize, 2);
        Assert.Equal(actual.Page, 1);
        Assert.Equal(actual.Items.Count, 2);
        Assert.Equal(actual.Items[0].Id, person1.Id);
        Assert.Equal(actual.Items[1].Id, person2.Id);
    }
}