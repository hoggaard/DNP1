using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;
    
    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ListPosts()
    {
        IQueryable<Post> posts = postRepository.GetManyAsync();
        Post[] postsArray = posts.ToArray();
        foreach (Post post in postsArray)
        {
            Console.WriteLine(post);
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