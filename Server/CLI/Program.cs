
using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");

IUserRepository userRepository = new UserFileRepositories();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

CliApp app = new CliApp(userRepository, commentRepository, postRepository);
await app.StartAsync();