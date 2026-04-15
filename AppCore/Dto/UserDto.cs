using AppCore.Interfaces;

namespace AppCore.Dto;

public class UserDto
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Department { get; init; }
    public SystemUserStatus Status { get; init; }
    public IEnumerable<string> Roles { get; init; } 
}