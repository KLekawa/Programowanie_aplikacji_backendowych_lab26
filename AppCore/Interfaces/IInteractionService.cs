using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IInteractionService
{
    Task<InteractionDto> AddEmailAsync(Guid contactId, CreateEmailDto createEmailDto);
    Task<InteractionDto> AddSmsAsync(Guid contactId, CreateSmsDto createSmsDto);
    Task<InteractionDto> AddEMeetingAsync(Guid contactId, CreateMeetingDto createMeetingDto);
    Task<bool> DeleteAsync(Guid interactionId, Guid contactId);
    Task<InteractionDto> GetByIdAsync(Guid interactionId);
    Task<IEnumerable<InteractionDto>> GetByContactIdAsync(Guid contactId);
    Task<IEnumerable<InteractionDto>> GetByDatesAsync(Guid contactId, DateTime from, DateTime to);
    Task<IEnumerable<InteractionDto>> GetByTypeAsync(Guid contactId, InteractionType type);
    Task<PagedResult<InteractionDto>> GetAllPagedAsync(int page, int size);
    Task<InteractionDto> UpdateAsync(Guid id, UpdateInteractionDto updateInteractionDto);
}