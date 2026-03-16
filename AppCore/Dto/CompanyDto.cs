using AppCore.Models;

namespace AppCore.Dto;

public record CompanyDto : ContactBaseDto
{
    public string Name { get; set; }
    public string? NIP { get; set; }
    public string? REGON { get; set; }
    public string? KRS { get; set; }
    public string? Industry { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public string? Website { get; set; }
    public List<Person>? Employees { get; set; }
    public Person? PrimaryContact { get; set; }

    public static CompanyDto FromEntity(Company company) => new()
    {
        Name = company.Name,
        NIP = company.NIP,
        REGON = company.REGON,
        KRS = company.KRS,
        Industry = company.Industry,
        AnnualRevenue = company.AnnualRevenue,
        Website = company.Website,
        Employees = company.Employees,
        PrimaryContact = company.PrimaryContact,
    };
}

public record CreateCompanyDto(
    string Name,
    string? NIP,
    string? REGON,
    string? KRS,
    string? Industry,
    decimal? AnnualRevenue,
    string? Website,
    List<Person>? Employees,
    Person? PrimaryContact,
    AddressDto? Address,
    string Email,
    string Phone
)
{
    public static Company ToEntity(CreateCompanyDto company, Guid id) => new()
    {
        Id = id,
        Name = company.Name,
        NIP = company.NIP,
        REGON = company.REGON,
        KRS = company.KRS,
        Industry = company.Industry,
        AnnualRevenue = company.AnnualRevenue,
        Website = company.Website,
        Employees = company.Employees,
        PrimaryContact = company.PrimaryContact,
        Address = new Address()
        {
            City = company.Address.City,
            Country = company.Address.Country,
            PostalCode = company.Address.PostalCode,
            Street = company.Address.Street,
            Type = company.Address.Type
        },
        Email = company.Email,
        Phone = company.Phone
    };
}

public record UpdateCompanyDto(
    string? Name,
    string? NIP,
    string? REGON,
    string? KRS,
    string? Industry,
    decimal? AnnualRevenue,
    string? Website,
    List<Person>? Employees,
    Person? PrimaryContact,
    AddressDto? Address,
    string? Email,
    string? Phone,
    ContactStatus? Status
)
{
    public void ToApply(Company company)
    {
        if(Name is not null)
            company.Name = Name;
        if (NIP is not null)
            company.NIP = NIP;
        if (REGON is not null)
            company.REGON = REGON;
        if (KRS is not null)
            company.KRS = KRS;
        if (Industry is not null)
            company.Industry = Industry;
        if (AnnualRevenue is not null)
            company.AnnualRevenue = AnnualRevenue;
        if (Website is not null)
            company.Website = Website;
        if (Employees is not null)
            company.Employees = Employees;
        if (PrimaryContact is not null)
            company.PrimaryContact = PrimaryContact;
        if (Address is not null)
            company.Address = new Address()
            {
                Street = Address.Street,
                City = Address.City,
                Country = Address.Country,
                PostalCode = Address.PostalCode,
                Type = Address.Type
            };
        if(Email is not null)
            company.Email = Email;
        if (Phone is not null)
            company.Phone = Phone;
        if (Status.HasValue)
            company.Status = Status.Value;


    }
}