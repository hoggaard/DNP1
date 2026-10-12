using APIContracts;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController(IPostRepository postRepo, IUserRepository userRepo) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDTO>> GetSinglePost(int id)
    {
        try
        {
            var post = await postRepo.GetSingleAsync(id);

            return Ok(new PostDTO
            {
                Id = post.Id,
                Body =  post.Body,
                Title = post.Title,
                Likes = post.Likes,
                Dislikes = post.Dislikes,
                Created = post.Created,
                UserId =  post.UserId
            });
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PostDTO>>> GetAllPosts(
        [FromQuery] string? title,
        [FromQuery] int? userId,
        [FromQuery] string? userName)
    {
        var query = postRepo.GetManyAsync();
        if (!string.IsNullOrWhiteSpace(userName))
        {
            var user = userRepo.GetManyAsync()
                .FirstOrDefault(u => u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user is null)
            {
                return Ok(new List<PostDTO>());
            }

            query = query.Where(p => p.UserId == user.Id);
        }
        
        if (userId.HasValue)
        {
            query = query.Where(p => p.UserId == userId.Value);
        }
        
        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }
        
        var result = query.Select(post => new PostDTO
        {
            Id = post.Id,
            Body = post.Body,
            Title = post.Title,
            Likes = post.Likes,
            Dislikes = post.Dislikes,
            Created = post.Created,
            UserId = post.UserId
        }).ToList();

        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody] UpdatePostDTO postDto)
    {
        if (id != postDto.Id)
        {
            return BadRequest();
        }

        var post = await postRepo.GetSingleAsync(id);

        post.Title = postDto.Title;
        post.Body = postDto.Body;
        post.Created = postDto.Created;
        post.Likes = postDto.Likes;
        post.Dislikes = postDto.Dislikes;

        try
        {
            await postRepo.UpdateAsync(post);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        try
        {
            await postRepo.DeleteAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> AddPost([FromBody] CreatePostDTO postDto)
    {
        Post post = new()
        {
            Title = postDto.Title,
            Body = postDto.Body,
            UserId = postDto.UserId
        };
        
        Post created = await postRepo.AddAsync(post);

        PostDTO dto = new()
        {
            Id = post.Id,
            Body = post.Body,
            Title = post.Title,
            Likes = post.Likes,
            Dislikes = post.Dislikes,
            Created = post.Created,
            UserId =  post.UserId
        };
        return Created($"/api/posts/{created.Id}", dto);
    }
}