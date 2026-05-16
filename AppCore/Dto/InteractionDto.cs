using AppCore.Models;

namespace AppCore.Dto;

public record InteractionDto(
    Guid Id,
    Guid ContactId,
    InteractionType InteractionType,
    DateTime Date,
    string Content,
    string Subject
    );

public record CreateInteractionDto(
    InteractionType Type,
    DateTime Date,
    string Content,
    string? Subject
);