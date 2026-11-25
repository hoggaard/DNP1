using System.Diagnostics.CodeAnalysis;

namespace Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    
    //relationships
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Post> Posts { get; set; }

    public User(){}  // for EFC
    
    public override string ToString()
    {
        return $"Id: {Id}, Username: {Username}, Password: {Password}, Email: {Email}";
    }
}