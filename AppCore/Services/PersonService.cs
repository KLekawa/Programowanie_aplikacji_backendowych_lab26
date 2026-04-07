using AppCore.Dto;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services;

public class PersonService(IContactUntiOfWork unitOfWork) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var people = await unitOfWork.Persons.FindPagedAsync(page, size);
        var personDtos = people.Items
            .Select(PersonDto.FromEntity)
            .ToList();
        
        return new PagedResult<PersonDto>(personDtos, people.TotalCount, people.Page, people.PageSize);
    }

    public async Task<IEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId)
    {
        var people = await unitOfWork.Persons.FindByCompanyIdAsync(companyId);
        var personDtos = people
            .Select(PersonDto.FromEntity);

        return personDtos;
    }

    public async Task<Person> AddPerson(CreatePersonDto person)
    {
        var entity = CreatePersonDto.ToEntity(person, Guid.NewGuid());
        entity  = await unitOfWork.Persons.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        
        return entity;
    }

    public async Task<Person> UpdatePerson(UpdatePersonDto person, Guid id)
    {
        var existingPerson = await unitOfWork.Persons.FindByIdAsync(id);
        
        if(existingPerson == null)
            throw new ContactNotFoundException($"Osoba o id: {id} nie znaleziona");
        
        person.ApplyTo(existingPerson);
        
        if (person.EmployerId.HasValue)
        {
            var company = await unitOfWork.Companies.FindByIdAsync(person.EmployerId.Value);
            if (company is not null)
                existingPerson.Employer = company;
        }
        
        await unitOfWork.SaveChangesAsync();
        return existingPerson;
    }

    public async Task<PersonDto?> GetById(Guid id)
    {
        var entity = await unitOfWork.Persons.FindByIdAsync(id);

        if (entity is null)
            throw new ContactNotFoundException($"Osoba o id: {id} nie znaleziona");
        
        var entityDto = PersonDto.FromEntity(entity);
        return entityDto;
    }

    public async Task<bool> DeletePerson(Guid id)
    {
            await unitOfWork.Persons.RemoveByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
            return true;
    }

    public async Task<Note> AddNote(Guid id, CreateNoteDto note)
    {
        var existingPerson = await unitOfWork.Persons.FindByIdAsync(id);
        if (existingPerson == null)
            throw new ContactNotFoundException($"Osoba o id: {id} nie znaleziona");

        if (existingPerson.Notes is null)
        {
            existingPerson.Notes = new List<Note>();
        }
        
        var noteEntity = new Note()
        {
            Id = Guid.NewGuid(),
            Content = note.Content,
            CreatedAt = DateTime.Now,
            CreatedBy = null
        };
        
        existingPerson.Notes.Add(noteEntity);
        await unitOfWork.Persons.UpdateAsync(existingPerson);
        await unitOfWork.SaveChangesAsync();

        return noteEntity;
    }

    public async Task<PersonDto?> AddTag(Guid id, Tag tag)
    {
        var existingPerson = await unitOfWork.Persons.FindByIdAsync(id);
        if (existingPerson is null)
            throw new ContactNotFoundException($"Osoba o id: {id} nie znaleziona");
        
        existingPerson.Tags.Add(tag);
        await unitOfWork.SaveChangesAsync();
        
        return PersonDto.FromEntity(existingPerson);
    }

    public async Task<bool> DeleteNote(Guid personId, Guid noteId)
    {
        var existingPerson = await unitOfWork.Persons.FindByIdAsync(personId);
        if (existingPerson is null)
            throw new ContactNotFoundException($"Osoba o id: {personId} nie znaleziona");

        var result = await unitOfWork.Persons.DeleteNoteAsync(personId, noteId);
        if (!result)
            throw new NoteNotFoundException($"Notatka o id: {personId} nie znaleziona");
        
        await unitOfWork.SaveChangesAsync();
        return result;
    }
}