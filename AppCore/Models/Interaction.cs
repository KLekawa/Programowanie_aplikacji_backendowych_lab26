namespace AppCore.Models;

public class Interaction : EntityBase
{
    public Guid Id { get; set; }
    
    public Contact Contact { get; set; }
    
    public Guid ContactId { get; set; }

    public InteractionType Type { get; set; }

    public DateTime Date { get; set; }

    public string Content { get; set; } = string.Empty;

    public string? Subject { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum InteractionType
{
    Email,
    Sms,
    Meeting
}