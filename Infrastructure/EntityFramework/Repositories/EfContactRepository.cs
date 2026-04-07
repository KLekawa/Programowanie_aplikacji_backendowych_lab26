using AppCore.Dto;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfContactRepository(ContactsDbContext context) : EfGenericRepository<Contact>(context.Set<Contact>()), IContactRepositoryAsync
{
    public Task<PagedResult<Contact>> SearchAsync(ContactBaseDto contactBaseDto)
    {
        var query = context.Set<Contact>().AsEnumerable();
        
        if(contactBaseDto.Id != Guid.Empty)
            query = query.Where(c => c.Id == contactBaseDto.Id);
        
        if(!string.IsNullOrWhiteSpace(contactBaseDto.Email))
            query = query.Where(c => c.Email == contactBaseDto.Email);
        
        if(!string.IsNullOrWhiteSpace(contactBaseDto.Phone))
            query = query.Where(c => c.Phone == contactBaseDto.Phone);

        if (contactBaseDto.Address is not null)
            query = query.Where(c =>
                c.Address.Street == contactBaseDto.Address.Street &&
                c.Address.City == contactBaseDto.Address.City &&
                c.Address.PostalCode == contactBaseDto.Address.PostalCode &&
                c.Address.Country == contactBaseDto.Address.Country &&
                c.Address.Type == contactBaseDto.Address.Type
            );
        
        if(contactBaseDto.Status is not null)
            query = query.Where(c => c.Status == contactBaseDto.Status);
        
        if(contactBaseDto.CreatedAt is not null)
            query = query.Where(c => c.CreatedAt == contactBaseDto.CreatedAt);

        if (contactBaseDto.Tags.Any())
        {
            query = query.Where(c =>
                contactBaseDto.Tags.All(tag =>
                    c.Tags.Any(t => t.Name == tag)));
        }

        var filtered = query.ToList();
        var totalCount =  filtered.Count();
        var result = new PagedResult<Contact>(filtered, totalCount, 1, totalCount);
        
        return Task.FromResult(result);
    }

    public async Task<Contact?> FindByTagAsync(Tag tag)
    {
        return await context.Set<Contact>().FirstOrDefaultAsync(c => c.Tags.Any(t => t.Name == tag.Name));
    }

    public async Task<Contact> AddNoteAsync(Note note, Guid contactId)
    {
        var entity = await context.Set<Contact>().FindAsync(contactId);
        if(entity is null)
            throw new ContactNotFoundException($"Kontakt o id {contactId} Nie znaleziona");
        
        entity.Notes ??= new List<Note>();
        entity.Notes.Add(note);
        return entity;
    }

    public async Task<IEnumerable<Note>> GetNotesByIdAsync(Guid contactId)
    {
        var entity = await context.Set<Contact>().FindAsync(contactId);
        if(entity is null)
            throw new ContactNotFoundException($"Kontakt o id {contactId} Nie znaleziona");
        return entity.Notes?.ToList() ?? new List<Note>();
    }

    public async Task<Contact> AddTagAsync(Tag tag, Guid contactId)
    {
        var entity = await context.Set<Contact>().FindAsync(contactId);
        if(entity is null)
            throw new ContactNotFoundException($"Kontakt o id {contactId} Nie znaleziona");
        entity.Tags.Add(tag);
        return entity;
    }

    public async Task<Contact> RemoveTagAsync(Tag tag, Guid contactId)
    {
        var entity = await context.Set<Contact>().FindAsync(contactId);
        if (entity is null)
            throw new ContactNotFoundException($"Kontakt o id {contactId} Nie znaleziona");
        entity.Tags.Remove(tag);
        return entity;
    }
}