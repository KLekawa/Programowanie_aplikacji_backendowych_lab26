using AppCore.Models;

namespace AppCore.Dto;

public class InteractionDto
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }

    public InteractionType Type { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }

    public string Details { get; set; }

    public static InteractionDto FromEntity(Interaction interaction)
    {
        return interaction switch
        {
            SmsInteraction sms => new InteractionDto
            {
                Id = sms.Id,
                ContactId = sms.ContactId,
                Type = InteractionType.Sms,
                Date = sms.Date,
                Content = sms.Content,
                Details = sms.PhoneNumber
            },

            EmailInteraction email => new InteractionDto
            {
                Id = email.Id,
                ContactId = email.ContactId,
                Type = InteractionType.Email,
                Date = email.Date,
                Content = email.Content,
                Details = $"{email.EmailAddress}, {email.Subject}"
            },

            MeetingInteraction meeting => new InteractionDto
            {
                Id = meeting.Id,
                ContactId = meeting.ContactId,
                Type = InteractionType.Meeting,
                Date = meeting.Date,
                Content = meeting.Content,
                Details = $"{meeting.Location}, {meeting.DurationMinutes} min"
            },
            _ => throw new ArgumentException("Unknown interaction type")
        };
    }
}