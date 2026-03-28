using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/contacts")]
public class ContactConrtoller(IPersonService service) : ControllerBase
{
    public async Task<IActionResult> GetAllPersons(int page, int size)
    {
        return Ok(await service.FindAllPeoplePaged(page, size));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {
        var dto = await service.GetById(id);
        if (dto is null)
            return NotFound();
        return Ok(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var result = await service.AddPerson(dto);
        return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePerson(Guid id, UpdatePersonDto dto)
    {
        var result = await service.UpdatePerson(dto, id);
        return Ok(result);
    }

    [HttpPost("{contactId:guid}/notes")]
    [ProducesResponseType(typeof(Note), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddNote(
        [FromRoute] Guid contactId,
        [FromBody] CreateNoteDto dto)
    {
        var note = await service.AddNote(contactId, dto);
        return CreatedAtAction(
            nameof(GetNotes),
            new { contactId },
            note);
    }

    [HttpGet("{contactId:guid}/notes")]
    [ProducesResponseType(typeof(IEnumerable<Note>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotes([FromRoute] Guid contactId)
    {
        var person = await service.GetById(contactId);
        return Ok(person.Notes);
    }
    
    [HttpDelete("{contactId:guid}/notes/{noteId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNote([FromRoute] Guid contactId, [FromRoute] Guid noteId)
    {
        await service.DeleteNote(contactId, noteId);
        return NoContent();
    }
}