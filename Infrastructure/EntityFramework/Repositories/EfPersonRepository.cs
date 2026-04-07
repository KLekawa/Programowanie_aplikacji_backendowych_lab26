using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfPersonRepository(ContactsDbContext context) : EfGenericRepository<Person>(context.People), IPersonRepositoryAsync
{
    public async Task<IEnumerable<Person>> FindByCompanyIdAsync(Guid companyId)
    {
        return await context.People.Where(p => p.Employer != null && p.Employer.Id == companyId).ToListAsync();
    }

    public async Task<IEnumerable<Person>> FindByOrganizationIdAsync(Guid organizationId)
    {
        return await context.People.Where(p => p.Organization != null && p.Organization.Id == organizationId).ToListAsync();
    }

    public async Task<bool> DeleteNoteAsync(Guid personId, Guid noteId)
    {
        var existingPerson = await context.People.Include(p => p.Notes).FirstOrDefaultAsync(p => p.Id == personId);
        // var existingPerson = await context.People.FirstOrDefaultAsync(p => p.Id == personId);
        if (existingPerson is null)
            throw new ContactNotFoundException($"Osoba o {personId} nie znaleziona");
        var note = existingPerson.Notes?.FirstOrDefault(n => n.Id == noteId);
        if (note is null)
            throw new NoteNotFoundException($"Notatka o {noteId} nie znaleziona"); 
        existingPerson.Notes!.Remove(note);
        return true;
    }
}