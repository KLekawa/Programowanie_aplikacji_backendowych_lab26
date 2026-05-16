namespace AppCore.Models;

public abstract class Interaction : EntityBase
{
    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }

    public DateTime Date { get; set; }
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class SmsInteraction : Interaction
{
    public string PhoneNumber { get; set; }
}

public class EmailInteraction : Interaction
{
    public string EmailAddress { get; set; }
    public string Subject { get; set; }
}

public class MeetingInteraction : Interaction
{
    public string Location { get; set; } 
    public int DurationMinutes { get; set; }
}
public enum InteractionType
{
    Sms,
    Email,
    Meeting
}