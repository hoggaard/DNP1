namespace APIContracts;

public class LoginUserDTO
{
    public required string Password { get; set; }
    public required string Username { get; set; }
}