using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;
    
    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task DeleteUser()
    {
        while (true)
        {
            Console.Write("ID on the user you wish to delete: ");
            string? rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }

            try
            {
                await userRepository.DeleteAsync(id);
                Console.WriteLine("User successfully deleted\n");
                break;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}