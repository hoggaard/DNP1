using APIContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserRepository userRepo) : ControllerBase
{
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<UserDTO>> Login([FromBody] LoginUserDTO request)
    {
        List<User> users = userRepo.GetManyAsync().ToList();
        foreach (var u in users)
        {
            if (u.Username.Equals(request.Username))
            {
                if (u.Password.Equals(request.Password))
                {
                    return new UserDTO()
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email
                    };
                }
            }
        }

        return Unauthorized();
    }
}