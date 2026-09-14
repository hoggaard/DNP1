using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    
    public SinglePostView(IPostRepository postRepository,  IUserRepository userRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ViewSinglePost()
    {
        while (true)
        {
            Console.Write("ID on the post you wish to view: ");
            string? rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }

            try
            {
                Post post = await postRepository.GetSingleAsync(id);
                Console.WriteLine(post);

                // while (true)
                // {
                //     Console.Write($"Do you wish to write a comment on the post? [y/n]: ");
                //     string? input = Console.ReadLine();
                //     if (input == "y")
                //     {
                //         while (true)
                //         {
                //             commentRepository.AddAsync()
                //         }
                //     } 
                //     else if (input == "n")
                //     {
                //         break;
                //     }
                // }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}