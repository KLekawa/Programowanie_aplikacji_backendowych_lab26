using AppCore.Models;

namespace AppCore.Dto;

public record OrganizationDto : ContactBaseDto
{
    public string Name { get; init; }
    public OrganizationType Type { get; init; }
    public string? KRS { get; init; }
    public string? Website { get; init; }
    public string? Mission { get; init; }
    public List<Person>? Members { get; init; }
    public Person? PrimaryContact { get; init; }

    public static OrganizationDto FromEntity(Organization organization) => new()
    {
        Name = organization.Name,
        Type = organization.Type,
        KRS = organization.KRS,
        Website = organization.Website,
        Mission = organization.Mission,
        PrimaryContact = organization.PrimaryContact,
        Members = organization.Members,
    };
}

public record CreateOrganizationDto(
    string Name,
    OrganizationType Type,
    string? KRS,
    string? Website,
    string? Mission,
    List<Person>? Members,
    Person? PrimaryContact,
    AddressDto? Address,
    string Email,
    string Phone
)
{
    public static Organization ToEntity(CreateOrganizationDto dto, Guid id) => new()
    {
        Id = id,
        Name = dto.Name,
        Type = dto.Type,
        KRS = dto.KRS,
        Website = dto.Website,
        Mission = dto.Mission,
        Members = dto.Members,
        PrimaryContact = dto.PrimaryContact,
        
        Address = dto.Address is null ? null : new Address()
        {
            City = dto.Address.City,
            Country = dto.Address.Country,
            Street = dto.Address.Street,
            PostalCode =  dto.Address.PostalCode,
            Type =  dto.Address.Type
        },
        Email = dto.Email,
        Phone = dto.Phone

    };
}

public record UpdateOrganizationDto(
    string? Name,
    OrganizationType? Type,
    string? KRS,
    string? Website,
    string? Mission,
    List<Person>? Members,
    Person? PrimaryContact,
    AddressDto? Address,
    ContactStatus? Status,
    string? Email,
    string? Phone
)
{
    public void ApplyTo(Organization organization)
    {
        if(Name is not null)
            organization.Name = Name;
        if(KRS is not null)
            organization.KRS = KRS;
        if(Website is not null)
            organization.Website = Website;
        if(Mission is not null)
            organization.Mission = Mission;
        if(Members is not null)
            organization.Members = Members;
        if(PrimaryContact is not null)
            organization.PrimaryContact = PrimaryContact;
        if (Address is not null)
            organization.Address = new Address()
            {
                City = Address.City,
                Country = Address.Country,
                Street = Address.Street,
                PostalCode = Address.PostalCode,
                Type = Address.Type
            };
        if(Status.HasValue)
            organization.Status = Status.Value;
        if(Email is not null)
            organization.Email = Email;
        if(Phone is not null)
            organization.Phone = Phone;
        if(Type.HasValue)
            organization.Type = Type.Value;
    }
}