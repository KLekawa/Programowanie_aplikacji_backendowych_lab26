using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IContactRepositoryAsync :  IGenericRepositoryAsync<Contact>
{
    Task<PagedResult<Contact>> SearchAsync(ContactBaseDto contactBaseDto);
    Task<Contact?> FindByTagAsync(Tag tag);
    Task<Contact> AddNoteAsync(Note note, Guid contactId);
    Task<IEnumerable<Note>> GetNotesByIdAsync(Guid contactId);
    Task<Contact> AddTagAsync(Tag tag, Guid contactId);
    Task<Contact> RemoveTagAsync(Tag tag, Guid contactId);

}