using AppCore.Dto;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;

namespace AppCore.Services;

public class InteractionService(IContactUntiOfWork unitOfWork) : IInteractionService
{
    public async Task<InteractionDto> AddEmailAsync(Guid contactId, CreateEmailDto createEmailDto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(contactId);
        if(person == null)
            throw new ContactNotFoundException($"Osoba o id: {contactId} nie znaleziona");
        
        var newInteraction = createEmailDto.ToEntity(contactId);
        newInteraction.Id = Guid.NewGuid();
        newInteraction.CreatedAt = DateTime.Now;
        
        if (person.Interactions is null)
            person.Interactions = new List<Interaction>();
        
        await unitOfWork.Interactions.AddAsync(newInteraction);
        await unitOfWork.SaveChangesAsync();

        return InteractionDto.FromEntity(newInteraction);
    }

    public async Task<InteractionDto> AddSmsAsync(Guid contactId, CreateSmsDto createSmsDto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(contactId);
        if(person == null)
            throw new ContactNotFoundException($"Osoba o id: {contactId} nie znaleziona");

        var newInteraction = createSmsDto.ToEntity(contactId);
        newInteraction.Id = Guid.NewGuid();
        newInteraction.CreatedAt = DateTime.Now;
        
        if (person.Interactions is null)
            person.Interactions = new List<Interaction>();
        
        await unitOfWork.Interactions.AddAsync(newInteraction);
        await unitOfWork.SaveChangesAsync();

        return InteractionDto.FromEntity(newInteraction);
    }

    public async Task<InteractionDto> AddEMeetingAsync(Guid contactId, CreateMeetingDto createMeetingDto)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(contactId);
        if(person == null)
            throw new ContactNotFoundException($"Osoba o id: {contactId} nie znaleziona");

        var newInteraction = createMeetingDto.ToEntity(contactId);
        newInteraction.Id = Guid.NewGuid();
        newInteraction.CreatedAt = DateTime.Now;
        
        if (person.Interactions is null)
            person.Interactions = new List<Interaction>();
        
        await unitOfWork.Interactions.AddAsync(newInteraction);
        await unitOfWork.SaveChangesAsync();

        return InteractionDto.FromEntity(newInteraction);
    }

    public async Task<bool> DeleteAsync(Guid interactionId, Guid contactId)
    {
        var person = await unitOfWork.Persons.FindByIdAsync(contactId);
        if(person == null)
            throw new ContactNotFoundException($"Osoba o id: {contactId} nie znaleziona");
        
        var interaction = await unitOfWork.Interactions.FindByIdAsync(interactionId);
        if(interaction is null || interaction.ContactId != contactId)
            throw new InteractionNotFoundException($"Interackja o id: {interactionId} nie znaleziona lub nie należy do tego kontaktu");
        
        await unitOfWork.Interactions.RemoveByIdAsync(interactionId);

        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<InteractionDto> GetByIdAsync(Guid interactionId)
    {
        var interaction = await unitOfWork.Interactions.FindByIdAsync(interactionId);
        if(interaction is null)
            throw new InteractionNotFoundException($"Interackja o id: {interactionId} nie znaleziona");
        
        return InteractionDto.FromEntity(interaction);
    }

    public async Task<IEnumerable<InteractionDto>> GetByContactIdAsync(Guid contactId)
    {
        var interactions = await unitOfWork.Interactions.GetByContactIdAsync(contactId);
        if(!interactions.Any())
            throw new InteractionNotFoundException($"Nie znaleziono żadnych interakcji");

        return interactions.Select(InteractionDto.FromEntity).ToList();
    }

    public async Task<IEnumerable<InteractionDto>> GetByDatesAsync(Guid contactId, DateTime from, DateTime to)
    {
        var interactions = await unitOfWork.Interactions.GetByDatesAsync(contactId, from, to);
        if(!interactions.Any())
            throw new InteractionNotFoundException($"Nie znaleziono żadnych interakcji");
        
        return interactions.Select(InteractionDto.FromEntity).ToList();
    }

    public async Task<IEnumerable<InteractionDto>> GetByTypeAsync(Guid contactId, InteractionType type)
    {
        var interactions = await unitOfWork.Interactions.GetByTypeAsync(contactId, type);
        if(!interactions.Any())
            throw new InteractionNotFoundException($"Nie znaleziono żadnych interakcji typu {type} dla kontaktu {contactId}");
        
        return interactions.Select(InteractionDto.FromEntity).ToList();
    }

    public async Task<PagedResult<InteractionDto>> GetAllPagedAsync(int page, int size)
    {
        var interactions = await unitOfWork.Interactions.FindPagedAsync(page, size);
        var interactionDtos = interactions.Items
            .Select(InteractionDto.FromEntity)
            .ToList();
        return new PagedResult<InteractionDto>(interactionDtos,  interactions.TotalCount, interactions.Page, interactions.PageSize);
    }

    public async Task<InteractionDto> UpdateAsync(Guid id, UpdateInteractionDto updateInteractionDto)
    {
        var interaction = await unitOfWork.Interactions.FindByIdAsync(id);
        if(interaction is null)
            throw new InteractionNotFoundException($"Interackja o id: {id} nie znaleziona");
        
        updateInteractionDto.ApplyTo(interaction);
        interaction.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync();
        return InteractionDto.FromEntity(interaction);
    }
}