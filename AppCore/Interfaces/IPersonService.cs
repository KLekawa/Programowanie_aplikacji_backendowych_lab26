using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<PersonDto> CreatePerson(CreatePersonDto person);
    Task<PersonDto?> UpdatePerson(UpdatePersonDto person, Guid id);
    Task<bool> DeletePerson(Guid id);
    Task<PersonDto?> AddNote(Guid id, Note note);
    Task<PersonDto?> AddTag(Guid id, Tag tag);
}