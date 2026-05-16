using AppCore.Models;

namespace AppCore.Dto;

public class CreateSmsDto
{
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public string PhoneNumber { get; set; }

    public SmsInteraction ToEntity(Guid contactId)
    {
        return new SmsInteraction
        {
            ContactId = contactId,
            Date = Date,
            Content = Content,
            PhoneNumber = PhoneNumber
        };
    }
}