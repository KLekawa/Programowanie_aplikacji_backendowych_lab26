using AppCore.Models;

namespace AppCore.Dto;

public record OrganizationDto : ContactBaseDto
{
    public string Name { get; init; }
    public OrganizationType Type { get; init; }
    public string? KRS { get; init; }
    public string? Website { get; init; }
    public string? Mission { get; init; }
    // public List<Person> Members { get; set; }
    public Person? PrimaryContact { get; set; }
}

public record CreateOrganizationDto
(
    string Name,
    OrganizationType Type,
    string? KRS,
    string? Website,
    string? Mission,
    // List<Person> Members,
    Person? PrimaryContact,
    AddressDto? Address,
    string Email,
    string Phone
);

public record UpdateOrganizationDto(
    string Name,
    OrganizationType Type,
    string? KRS,
    string? Website,
    string? Mission,
    // List<Person> Members,
    Person? PrimaryContact,
    AddressDto? Address,
    ContactStatus Status,
    string Email,
    string Phone
    );