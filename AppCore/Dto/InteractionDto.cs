using AppCore.Models;

namespace AppCore.Dto;

public class InteractionDto
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }

    public InteractionType Type { get; set; }

    public DateTime Date { get; set; }
    public string Content { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }
    public string? Subject { get; set; }

    public string? Location { get; set; }
    public int? DurationMinutes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static InteractionDto FromEntity(Interaction interaction)
    {
        return new InteractionDto
        {
            Id = interaction.Id,
            ContactId = interaction.ContactId,
            Type = interaction.Type,
            Date = interaction.Date,
            Content = interaction.Content,
            PhoneNumber = interaction.PhoneNumber,
            EmailAddress = interaction.EmailAddress,
            Subject = interaction.Subject,
            Location = interaction.Location,
            DurationMinutes = interaction.DurationMinutes,
            CreatedAt = interaction.CreatedAt,
            UpdatedAt = interaction.UpdatedAt
        };
    }
}