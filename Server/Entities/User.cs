using System.Diagnostics.CodeAnalysis;

namespace Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Username: {Username}, Password: {Password}, Email: {Email}";
    }
}