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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetSingleUser(int id)
    {
        try
        {
            var user = await userRepo.GetSingleAsync(id);

            return Ok(new UserDTO
            {
                Id = user.Id,
                Username =  user.Username,
                Email = user.Email
            });
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpGet]
    public ActionResult<List<UserDTO>> GetManyUsers()
    {
        var user = userRepo.GetManyAsync();
        var userDtos = new List<UserDTO>();
        user.ToList().ForEach(userDTO => userDtos.Add(new UserDTO
        {
            Id = userDTO.Id,
            Username = userDTO.Username,
            Email = userDTO.Email
        }));
        return Ok(userDtos);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UpdateUserDTO userDto)
    {
        if (id != userDto.Id)
        {
            return BadRequest();
        }

        var user = await userRepo.GetSingleAsync(id);

        user.Username = userDto.Username;
        user.Email = userDto.Email;
        user.Password = userDto.Password;

        try
        {
            await userRepo.UpdateAsync(user);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await userRepo.DeleteAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] CreateUserDTO request)
    {
        if (!await VerifyUserNameIsAvailableAsync(request.Username))
        {
            throw new Exception("Username is already taken");
        }
        User user = new()
        {
            Email = request.Email,
            Password = request.Password,
            Username =  request.Username
        };
        
        User created = await userRepo.AddAsync(user);

        UserDTO dto = new()
        {
            Id = created.Id,
            Username = created.Username,
            Email = created.Email
        };
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