using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;
    
    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public async Task DeletePost()
    {
        while (true)
        {
            Console.Write("ID on the post you wish to delete: ");
            string? rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }

            try
            {
                await postRepository.DeleteAsync(id);
                Console.WriteLine("Post successfully deleted\n");
                break;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}