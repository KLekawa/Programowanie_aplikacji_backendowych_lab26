namespace AppCore.Models;

public abstract class Interaction : EntityBase
{
    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }

    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class SmsInteraction : Interaction
{
    public string PhoneNumber { get; set; } = string.Empty;
}

public class EmailInteraction : Interaction
{
    public string EmailAddress { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
}

public class MeetingInteraction : Interaction
{
    public string Location { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
}