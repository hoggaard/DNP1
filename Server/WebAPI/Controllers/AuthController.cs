using APIContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        List<User> users = await userRepo.GetManyAsync().ToListAsync();
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