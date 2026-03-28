using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<Person> AddPerson(CreatePersonDto person);
    Task<Person> UpdatePerson(UpdatePersonDto person, Guid id);
    Task<PersonDto?> GetById(Guid id);
    Task<bool> DeletePerson(Guid id);
    Task<Note> AddNote(Guid id, CreateNoteDto note);
    Task<PersonDto?> AddTag(Guid id, Tag tag);
    Task<bool> DeleteNote(Guid personId, Guid noteId);
}