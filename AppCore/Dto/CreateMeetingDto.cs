using AppCore.Models;

namespace AppCore.Dto;

public class CreateMeetingDto
{
    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }

    public MeetingInteraction ToEntity(Guid contactId)
    {
        return new MeetingInteraction
        {
            ContactId = contactId,
            Date = Date,
            Content = Content,
            Location = Location,
            DurationMinutes = DurationMinutes
        };
    }
}