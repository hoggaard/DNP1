using APIContracts;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] CreateUserDTO request)
    {
        if (!await VerifyUserNameIsAvailableAsync(request.UserName))
        {
            throw new Exception("Username is already taken");
        }
        User user = new()
        {
            Email = request.Email,
            Password = request.Password,
            Username =  request.UserName
        };
        
        User created = await userRepo.AddAsync(user);
        
        UserDTO dto = new()
        {
            Id = created.Id,
            UserName = created.Username
        };
        Console.WriteLine(user);
        return Created($"/api/users/{created.Id}", dto);
    }

    private async Task<bool> VerifyUserNameIsAvailableAsync(string userName)
    {
        var users = userRepo.GetManyAsync().ToList();
        bool available = true;
        users.ForEach(user =>
        {
            if (user.Username.Equals(userName))
            {
                available = false;
            }
        });
        return available;
    }
}