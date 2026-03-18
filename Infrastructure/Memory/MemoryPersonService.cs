using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonService(IContactUntiOfWork unitOfWork) : IPersonService
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
            return null;
        
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
            return null;
        
        var entityDto = PersonDto.FromEntity(entity);
        return entityDto;
    }

    public async Task<bool> DeletePerson(Guid id)
    {
        try
        {
            await unitOfWork.Persons.RemoveByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
    }

    public async Task<PersonDto?> AddNote(Guid id, Note note)
    {
        var existingPerson = await unitOfWork.Persons.FindByIdAsync(id);
        if(existingPerson == null)
            return null;
        
        existingPerson.Notes.Add(note);
        await unitOfWork.SaveChangesAsync();
        
        return PersonDto.FromEntity(existingPerson);
    }

    public async Task<PersonDto?> AddTag(Guid id, Tag tag)
    {
        var existingPerson = await unitOfWork.Persons.FindByIdAsync(id);
        if (existingPerson is null)
            return null;
        
        existingPerson.Tags.Add(tag);
        await unitOfWork.SaveChangesAsync();
        
        return PersonDto.FromEntity(existingPerson);
    }
}