using AppCore.Models;

namespace AppCore.Dto;

public class CreateSmsDto
{
    public DateTime Date { get; set; }
    public string Content { get; set; }

    public string PhoneNumber { get; set; }

    public Interaction ToEntity(Guid contactId)
    {
        return new Interaction
        {
            ContactId = contactId,
            Type = InteractionType.Sms,
            Date = Date,
            Content = Content,
            PhoneNumber = PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };
    }
}