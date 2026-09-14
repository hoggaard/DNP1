using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;
    
    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ListUsers()
    {
        IQueryable<User> users = userRepository.GetManyAsync();
        User[] usersArray = users.ToArray();
        foreach (User user in usersArray)
        {
            Console.WriteLine(user);
        }
        Console.WriteLine();
        while (true)
        {
            Console.Write("Type -1 to go back: ");
            string? rawInput = Console.ReadLine();
            if (int.TryParse(rawInput, out int id))
            {
                if (id != -1)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                }
                else
                {
                    break;
                }
            }
        }
    }
}