using AppCore.Models;

namespace AppCore.Dto;

public class CreateEmailDto
{
    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;

    public EmailInteraction ToEntity(Guid contactId)
    {
        return new EmailInteraction
        {
            ContactId = contactId,
            Date = Date,
            Content = Content,
            EmailAddress = EmailAddress,
            Subject = Subject
        };
    }
}