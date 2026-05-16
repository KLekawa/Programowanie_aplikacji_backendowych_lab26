namespace AppCore.Models;

public class Interaction : EntityBase
{
    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }

    public InteractionType Type { get; set; }

    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }
    public string? Subject { get; set; }

    public string? Location { get; set; }
    public int? DurationMinutes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum InteractionType
{
    Email,
    Sms,
    Meeting
}