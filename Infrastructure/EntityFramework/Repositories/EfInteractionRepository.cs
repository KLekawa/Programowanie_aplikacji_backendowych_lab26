using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Interaction = AppCore.Models.Interaction;

namespace Infrastructure.EntityFramework.Repositories;

public class EfInteractionRepository(ContactsDbContext context) : EfGenericRepository<Interaction>(context.Interactions), IInteractionRepositoryAsync
{
    public async Task<IEnumerable<Interaction>> GetByContactIdAsync(Guid contactId)
    {
        return await context.Interactions.Where(i => i.ContactId == contactId).ToListAsync();
    }

    public async Task<IEnumerable<Interaction>> GetByDatesAsync(Guid contactId, DateTime from, DateTime to)
    {
        return await context.Interactions.Where(i => i.ContactId == contactId && i.Date <= to && i.Date >= from).ToListAsync();
    }

    public async Task<IEnumerable<Interaction>> GetByTypeAsync(Guid contactId, InteractionType type)
    {
        return await context.Interactions.Where(i => i.ContactId == contactId && i.Type == type).ToListAsync();
    }
}