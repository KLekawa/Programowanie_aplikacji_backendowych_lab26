using AppCore.Models;

namespace AppCore.Dto;

public record CompanyDto : ContactBaseDto
{
    public string Name { get; set; }
    public string? NIP { get; set; }
    public string? REGON { get; set; }
    public string? KRS { get; set; }
    public string? Industry { get; set; }
    // public decimal? AnnualRevenue { get; set; }
    public string? Website { get; set; }
    // public List<Person> Employees { get; set; }
    public Person? PrimaryContact { get; set; }
}

public record CreateCompanyDto(
    string Name,
    string? NIP,
    string? REGON,
    string? KRS,
    string? Industry,
    // decimal? AnnualRevenue,
    string? Website,
    // public List<Person> Employees { get; set; }
    Person? PrimaryContact,
    AddressDto? Address,
    string Email,
    string Phone
);

public record UpdateCompanyDto(
    string Name,
    string? NIP,
    string? REGON,
    string? KRS,
    string? Industry,
    // decimal? AnnualRevenue,
    string? Website,
    // public List<Person> Employees { get; set; }
    Person? PrimaryContact,
    AddressDto? Address,
    string Email,
    string Phone,
    ContactStatus Status
);