using AppCore.Interfaces;
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
}