using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    
    public CreatePostView(IPostRepository postRepository,  IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }
    
    public async Task<Post> CreatePost()
    {
        Console.Write("title: ");
        string? title = Console.ReadLine();
        Console.WriteLine();
        Console.Write("body: ");
        string? body = Console.ReadLine();
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
        {
            throw new Exception("Username or Password or Email is empty");
        }

        User? user = null;
        while (user == null)
        {
            Console.Write("UserID on the author: ");
            string? rawInput = Console.ReadLine();
            if (int.TryParse(rawInput, out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }

            try
            {
                User mUser = await userRepository.GetSingleAsync(id);
                user = mUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        Post post = new Post
        {
            Title = title,
            Body = body,
            Dislikes = 0,
            Likes = 0,
            UserId = user.Id,
            Created = DateTime.UtcNow
        };
        await postRepository.AddAsync(post);
        Console.WriteLine("Post created successfully");
        return post;
    }
}