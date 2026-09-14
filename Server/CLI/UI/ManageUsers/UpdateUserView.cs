using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository userRepository;
    
    public UpdateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task<User> UpdateUser()
    {
        while (true)
        {
            Console.Write("ID on the user you wish to update: ");
            string? rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }

            try
            {
                User user = await userRepository.GetSingleAsync(id);
                Console.WriteLine("---- Leave empty for no change ----");
                Console.Write($"Username ({user.Username}): ");
                string? username = Console.ReadLine();
                username = string.IsNullOrWhiteSpace(username) ? user.Username : username.Trim();
                Console.Write($"Email ({user.Email}): ");
                string? email = Console.ReadLine();
                email = string.IsNullOrWhiteSpace(email) ? user.Email : email.Trim();
                Console.Write($"Password ({user.Password}): ");
                string? password = Console.ReadLine();
                password = string.IsNullOrWhiteSpace(password) ? user.Password : password;
                User newUser = new User
                {
                    Id = id,
                    Username = username,
                    Password = password,
                    Email = email,
                };
                await userRepository.UpdateAsync(newUser);
                return newUser;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}