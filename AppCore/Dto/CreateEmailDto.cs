using AppCore.Models;

namespace AppCore.Dto;

public class CreateEmailDto
{
    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;

    public Interaction ToEntity(Guid contactId)
    {
        return new Interaction
        {
            ContactId = contactId,
            Type = InteractionType.Email,
            Date = Date,
            Content = Content,
            EmailAddress = EmailAddress,
            Subject = Subject,
            CreatedAt = DateTime.UtcNow
        };
    }
}