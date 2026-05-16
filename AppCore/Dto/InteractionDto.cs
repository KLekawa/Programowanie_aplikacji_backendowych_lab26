using AppCore.Models;

namespace AppCore.Dto;

public class InteractionDto
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }

    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public static InteractionDto FromEntity(Interaction interaction)
    {
        return interaction switch
        {
            SmsInteraction sms => new InteractionDto
            {
                Id = sms.Id,
                ContactId = sms.ContactId,
                Type = "Sms",
                Date = sms.Date,
                Content = sms.Content,
                Details = sms.PhoneNumber
            },

            EmailInteraction email => new InteractionDto
            {
                Id = email.Id,
                ContactId = email.ContactId,
                Type = "Email",
                Date = email.Date,
                Content = email.Content,
                Details = $"{email.EmailAddress}, {email.Subject}"
            },

            MeetingInteraction meeting => new InteractionDto
            {
                Id = meeting.Id,
                ContactId = meeting.ContactId,
                Type = "Meeting",
                Date = meeting.Date,
                Content = meeting.Content,
                Details = $"{meeting.Location}, {meeting.DurationMinutes} min"
            },

            _ => new InteractionDto
            {
                Id = interaction.Id,
                ContactId = interaction.ContactId,
                Type = "Unknown",
                Date = interaction.Date,
                Content = interaction.Content
            }
        };
    }
}