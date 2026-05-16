using AppCore.Models;

namespace AppCore.Dto;

public class UpdateInteractionDto
{
    public DateTime? Date { get; set; }
    public string? Content { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }
    public string? Subject { get; set; }

    public string? Location { get; set; }
    public int? DurationMinutes { get; set; }

    public void ApplyTo(Interaction interaction)
    {
        if (Date is not null)
            interaction.Date = Date.Value;

        if (Content is not null)
            interaction.Content = Content;

        if (interaction is SmsInteraction sms)
        {
            if (PhoneNumber is not null)
                sms.PhoneNumber = PhoneNumber;
        }

        if (interaction is EmailInteraction email)
        {
            if (EmailAddress is not null)
                email.EmailAddress = EmailAddress;

            if (Subject is not null)
                email.Subject = Subject;
        }

        if (interaction is MeetingInteraction meeting)
        {
            if (Location is not null)
                meeting.Location = Location;

            if (DurationMinutes is not null)
                meeting.DurationMinutes = DurationMinutes.Value;
        }
    }
}