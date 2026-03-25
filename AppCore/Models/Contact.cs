namespace AppCore.Models;

public abstract class Contact : EntityBase
{
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public Address? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ContactStatus Status { get; set; }
    public List<Tag> Tags { get; set; } = new();
    public List<Note>? Notes { get; set; }


    public abstract string GetDisplayName();
}

public class Note : EntityBase
{
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class Tag : EntityBase
{
    public string Name { get; set; }
    public string Color { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public AddressType Type { get; set; }
}

public enum AddressType
{
    Main, Correspondence, Delivery, Billing
}

public enum ContactStatus
{
    Active, Inactive, Blocked, Prospect, Lead
}