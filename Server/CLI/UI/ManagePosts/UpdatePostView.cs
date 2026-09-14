using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;
    
    public UpdatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task<Post> UpdatePost()
    {
        while (true)
        {
            Console.Write("ID on the post you wish to update: ");
            string? rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
            }

            try
            {
                Post post = await postRepository.GetSingleAsync(id);
                Console.WriteLine("---- Leave empty for no change ----");
                Console.Write($"Title ({post.Title}): ");
                string? title = Console.ReadLine();
                title = string.IsNullOrWhiteSpace(title) ? post.Title : title.Trim();
                Console.Write($"Body ({post.Body}): ");
                string? body = Console.ReadLine();
                body = string.IsNullOrWhiteSpace(body) ? post.Body : body.Trim();
                
                Post newPost = new Post
                {
                    Id = id,
                    Title = title,
                    Body = body,
                    UserId = post.UserId,
                    Created = post.Created,
                    Dislikes = post.Dislikes,
                    Likes = post.Likes,
                };
                await postRepository.UpdateAsync(newPost);
                Console.WriteLine("Successfully updated the post!");
                return newPost;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}