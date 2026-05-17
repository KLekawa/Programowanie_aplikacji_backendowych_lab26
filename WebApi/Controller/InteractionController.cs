using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/interactions")]
public class InteractionController(IInteractionService service) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllPaged(int page, int size)
    {
        return Ok(await service.GetAllPagedAsync(page, size));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await service.GetByIdAsync(id);
        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost("contacts/{contactId:guid}/email")]
    public async Task<IActionResult> AddEmail(Guid contactId, CreateEmailDto dto)
    {
        var result = await service.AddEmailAsync(contactId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("contacts/{contactId:guid}/sms")]
    public async Task<IActionResult> AddSms(Guid contactId, CreateSmsDto dto)
    {
        var result = await service.AddSmsAsync(contactId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("contacts/{contactId:guid}/meeting")]
    public async Task<IActionResult> AddMeeting(Guid contactId, CreateMeetingDto dto)
    {
        var result = await service.AddEMeetingAsync(contactId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("contacts/{contactId:guid}")]
    public async Task<IActionResult> GetByContactId(Guid contactId)
    {
        return Ok(await service.GetByContactIdAsync(contactId));
    }

    [HttpGet("contacts/{contactId:guid}/dates")]
    public async Task<IActionResult> GetByDates(Guid contactId, DateTime from, DateTime to)
    {
        return Ok(await service.GetByDatesAsync(contactId, from, to));
    }

    [HttpGet("contacts/{contactId:guid}/type/{type}")]
    public async Task<IActionResult> GetByType(Guid contactId, InteractionType type)
    {
        return Ok(await service.GetByTypeAsync(contactId, type));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateInteractionDto dto)
    {
        var result = await service.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("contacts/{contactId:guid}/{interactionId:guid}")]
    public async Task<IActionResult> Delete(Guid contactId, Guid interactionId)
    {
        await service.DeleteAsync(interactionId, contactId);
        return NoContent();
    }
}