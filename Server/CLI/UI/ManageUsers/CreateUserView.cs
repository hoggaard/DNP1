using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;
    
    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<User> CreateUser()
    {
        Console.Write("username: ");
        string? username = Console.ReadLine();
        Console.WriteLine();
        Console.Write("password: ");
        string? password = Console.ReadLine();
        Console.WriteLine();
        Console.Write("email: ");
        string? email = Console.ReadLine();
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            throw new Exception("Username or Password or Email is empty");
        }
        User user = new User
        {
            Username = username.ToLower(),
            Password = password,
            Email = email.ToLower()
        };
        await userRepository.AddAsync(user);
        Console.WriteLine("User created successfully");
        return user;
    }
}