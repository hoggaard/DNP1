using APIContracts;

namespace BlazorApp1.Services;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(CreateUserDTO request);
}