using APIContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController(ICommentRepository commentRepo, IUserRepository userRepo) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDTO>> GetSingleComment(int id)
    {
        try
        {
            var comment = await commentRepo.GetSingleAsync(id);

            return Ok(new CommentDTO
            {
                Id = comment.Id,
                Body =  comment.Body,
                Likes = comment.Likes,
                Dislikes = comment.Dislikes,
                Created = comment.Created,
                PostId = comment.PostId,
                UserId = comment.UserId
            });
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CommentDTO>>> GetAllComments(
        [FromQuery] string? userName,
        [FromQuery] int? postId,
        [FromQuery] int? userId)
    {
        try
        {
            var query = commentRepo.GetManyAsync();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = userRepo.GetManyAsync()
                    .FirstOrDefault(u => u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (user is null)
                {
                    return Ok(new List<CommentDTO>());
                }

                query = query.Where(p => p.UserId == user.Id);
            }

            if (postId.HasValue)
            {
                query = query.Where(p => p.PostId == postId.Value);
            }
            
            if (userId.HasValue)
            {
                query = query.Where(p => p.UserId == userId.Value);
            }
            
            var comments = new List<CommentDTO>();
            query.ToList().ForEach(comment => comments.Add(new CommentDTO
            {
                Id = comment.Id,
                Body = comment.Body,
                Likes = comment.Likes,
                Dislikes = comment.Dislikes,
                Created = comment.Created,
                PostId = comment.PostId,
                UserId = comment.UserId
            }));
            
            return Ok(comments);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CommentDTO>> UpdateComment(int id, [FromBody] UpdateCommentDTO commentDto)
    {
        if (id != commentDto.Id)
        {
            return BadRequest();
        }

        var comment = await commentRepo.GetSingleAsync(id);

        comment.Body = commentDto.Body;
        comment.Created = commentDto.Created;

        try
        {
            await commentRepo.UpdateAsync(comment);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        try
        {
            await commentRepo.DeleteAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO commentDto)
    {
        Comment comment = new()
        {
            Body = commentDto.Body,
            UserId = commentDto.UserId,
            PostId = commentDto.PostId
        };
        
        Comment created = await commentRepo.AddAsync(comment);

        CommentDTO dto = new()
        {
            Id = comment.Id,
            Body = comment.Body,
            Likes = comment.Likes,
            Dislikes = comment.Dislikes,
            Created = comment.Created,
            PostId = comment.PostId,
            UserId = comment.UserId
        };
        return Created($"/api/comments/{created.Id}", dto);
    }
}