namespace APIContracts;

public class CreateUserDTO
{
    public required string Password { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}