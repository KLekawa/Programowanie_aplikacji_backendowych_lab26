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

    public Interaction ToEntity(Interaction interaction)
    {
        if (Date is not null)
            interaction.Date = Date.Value;

        if (Content is not null)
            interaction.Content = Content;

        if (PhoneNumber is not null)
            interaction.PhoneNumber = PhoneNumber;

        if (EmailAddress is not null)
            interaction.EmailAddress = EmailAddress;

        if (Subject is not null)
            interaction.Subject = Subject;

        if (Location is not null)
            interaction.Location = Location;

        if (DurationMinutes is not null)
            interaction.DurationMinutes = DurationMinutes.Value;

        interaction.UpdatedAt = DateTime.UtcNow;

        return interaction;
    }
}