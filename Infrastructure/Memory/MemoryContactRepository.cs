using AppCore.Dto;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryContactRepository : MemoryGenericRepository<Contact>, IContactRepositoryAsync
{
    public Task<PagedResult<Contact>> SearchAsync(ContactBaseDto contactBaseDto)
    {
        var query = _data.Values.AsEnumerable();
        
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

    public Task<Contact?> FindByTagAsync(Tag tag)
    {
        var result = _data.Values.FirstOrDefault(c => c.Tags.Contains(tag));
        
        return Task.FromResult(result);
    }

    public Task<Contact> AddNoteAsync(Note note, Guid contactId)
    {
        _data[contactId].Notes.Add(note);
        return Task.FromResult(_data[contactId]);
    }

    public Task<IEnumerable<Note>> GetNotesByIdAsync(Guid contactId)
    {
        var result = _data[contactId].Notes.AsEnumerable();
        return Task.FromResult(result);
    }

    public Task<Contact> AddTagAsync(Tag tag, Guid contactId)
    {
        _data[contactId].Tags.Add(tag);
        return Task.FromResult(_data[contactId]);
    }

    public Task<Contact> RemoveTagAsync(Tag tag, Guid contactId)
    {
        if(_data[contactId].Tags.Remove(tag))
            throw new ContactNotFoundException($"Osoba o id: {contactId} nie znaleziona");
        return Task.FromResult(_data[contactId]);
    }
}