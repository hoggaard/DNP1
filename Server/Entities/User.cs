namespace Entities;

public class User
{
    public int Id { get; set; }
    public int Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}