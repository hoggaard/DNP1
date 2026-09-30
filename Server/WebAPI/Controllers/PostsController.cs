using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private IPostRepository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }
    
    [HttpGet("posts/{id}")]
    public async Task<ActionResult<Post>> GetSingle(int id)
    {
        var post = await postRepo.GetSingleAsync(id);
        Console.WriteLine(post);
        return post;
    }
}