using AppCore.Models;

namespace AppCore.Interfaces;

public interface IInteractionRepositoryAsync : IGenericRepositoryAsync<Interaction>
{
    Task<IEnumerable<Interaction>> GetByContactIdAsync(Guid contactId);
    Task<IEnumerable<Interaction>> GetByDatesAsync(Guid contactId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<Interaction>> GetByTypeAsync(Guid contactId, InteractionType type);

}