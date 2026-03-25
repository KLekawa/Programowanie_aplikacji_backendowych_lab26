using System.Drawing;
using AppCore.Models;

namespace AppCore.Dto;

public abstract record ContactBaseDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string? Phone { get; init; }
    public AddressDto? Address { get; init; }
    public ContactStatus? Status { get; init; }
    public List<string> Tags { get; init; } = new();
    public List<Note> Notes { get; init; }
    public DateTime? CreatedAt { get; init; }
    
    // public static ContactBaseDto FromEntity(Contact contact) => new()
    // {
    //     Id = contact.Id,
    //     Email = contact.Email,
    //     Phone = contact.Phone,
    //     Address =  new AddressDto(contact.Address.Street, contact.Address.City, contact.Address.PostalCode, contact.Address.Country, contact.Address.Type),
    //     Status = contact.Status,
    //     Tags = contact.Tags.Select(t => t.Name).ToList(),
    //     CreatedAt = contact.CreatedAt
    // };
}


// public abstract record CreateContactDto
// {
//     public string Email { get; init; }
//     public string Phone { get; init; }
//     public AddressDto? Address { get; init; }
//     public ContactStatus Status { get; init; }
//     public List<string> Tags { get; init; } = new();
//
//     public static Contact ToEntity(CreateContactDto dto, Guid id) => new()
//     {
//         Id = id,
//         Email = dto.Email,
//         Phone = dto.Phone,
//         Address = new Address
//         {
//             Street = dto.Address.Street,
//             City = dto.Address.City,
//             PostalCode = dto.Address.PostalCode,
//             Country = dto.Address.Country,
//             Type = dto.Address.Type
//         },
//         Status = dto.Status,
//         Tags = dto.Tags.Select(t => new Tag { Name = t }).ToList(),
//         
//
//     }
// }

public record AddressDto(
    string Street,
    string City,
    string PostalCode,
    string Country,
    AddressType Type
);

public record CreateNoteDto
{
    public string Content { get; init; }
}

