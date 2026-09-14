using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
{
    public IUserRepository UserRepository { get; set; } = userRepository;
    public ICommentRepository CommentRepository { get; set; } = commentRepository;
    public IPostRepository PostRepository { get; set; } = postRepository;
    
    public async Task StartAsync()
    {
        bool running = true;
        while(running)
        {
            Console.WriteLine("---- Options (-1 to close) ----");
            Console.WriteLine("---- User ----");
            Console.WriteLine("1) List Users");
            Console.WriteLine("2) Create User");
            Console.WriteLine("3) Update User");
            Console.WriteLine("4) Delete User");
            Console.WriteLine("---- Post ----");
            Console.WriteLine("5) List Posts");
            Console.WriteLine("6) Create Post");
            Console.WriteLine("7) Update Post");
            Console.WriteLine("8) Delete Post");
            Console.WriteLine("9) View Single Post");
            Console.WriteLine("");
            Console.Write("Input: ");
            
            string? rawInput = Console.ReadLine();
            if (!int.TryParse(rawInput, out int input))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
                continue;
            }
            
            switch (input)
            {
                case 1:
                    ListUsersView listUsersView = new(UserRepository);
                    await listUsersView.ListUsers();
                    break;
                case 2:
                    CreateUserView createUserView = new(UserRepository);
                    await createUserView.CreateUser();
                    break;
                case 3:
                    UpdateUserView updateUserView = new(UserRepository);
                    await updateUserView.UpdateUser();
                    break;
                case 4:
                    DeleteUserView deleteUserView = new(UserRepository);
                    await deleteUserView.DeleteUser();
                    break;
                case 5:
                    ListPostsView listPostsView = new(PostRepository);
                    await listPostsView.ListPosts();
                    break;
                case 6:
                    CreatePostView createPostView = new(PostRepository, userRepository);
                    await createPostView.CreatePost();
                    break;
                case 7:
                    UpdatePostView updatePostView = new(PostRepository);
                    await updatePostView.UpdatePost();
                    break;
                case 8:
                    DeletePostView deletePostView = new(PostRepository);
                    await deletePostView.DeletePost();
                    break;
                case 9:
                    SinglePostView singlePostView = new(PostRepository, userRepository, commentRepository);
                    await singlePostView.ViewSinglePost();
                    break;
                case -1:
                    running = false;
                    break;
            }
        }
    }
}