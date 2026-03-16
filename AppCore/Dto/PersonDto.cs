using AppCore.Models;

namespace AppCore.Dto;

public record PersonDto : ContactBaseDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Position { get; init; }
    public DateTime? BirthDate { get; init; }
    public Gender Gender { get; init; }
    public Guid? EmployerId { get; init; }

    public static PersonDto FromEntity(Person person) => new()
    {
        FirstName = person.FirstName,
        LastName = person.LastName,
        Position = person.Position,
        BirthDate = person.BirthDate,
        Gender = person.Gender,
        EmployerId = person.Employer?.Id,
    };
}

public record CreatePersonDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string? Position,
    DateTime? BirthDate,
    Gender Gender,
    Guid? EmployerId,
    AddressDto? Address
)
{
    public static Person ToEntity(CreatePersonDto dto, Guid id) => new()
    {
        Id = id,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Email = dto.Email,
        Phone = dto.Phone,
        Position = dto.Position,
        BirthDate = dto.BirthDate,
        Gender = dto.Gender,
        Address = new Address()
        {
            Street = dto.Address.Street,
            City = dto.Address.City,
            PostalCode = dto.Address.PostalCode,
            Country = dto.Address.Country,
            Type = dto.Address.Type
        }
    };
}


public record UpdatePersonDto(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Position,
    DateTime? BirthDate,
    Gender? Gender,
    Guid? EmployerId,
    AddressDto? Address,
    ContactStatus? Status
)
{
    public void ApplyTo(Person person) 
    {
        if(FirstName is not null)
            person.FirstName = FirstName;
        if(LastName is not null)
            person.LastName = LastName;
        if(Email is not null)
            person.Email = Email;
        if(Phone is not null)
            person.Phone = Phone;
        if(Position is not null)
            person.Position = Position;
        if(BirthDate is not null)
            person.BirthDate = BirthDate;
        if(Gender is not null)
            person.Gender = Gender.Value;
      // Employer =  
      if (Address is not null)
      {
          person.Address = new Address()
          {
              Street = Address.Street,
              City = Address.City,
              PostalCode = Address.PostalCode,
              Country = Address.Country,
              Type = Address.Type
          };
      }

      if(Status is not null)
          person.Status = Status.Value;
      
    }
};