using APIContracts;

namespace BlazorApp1.Services;

public interface ICommentService
{
    public Task<CommentDTO> AddCommentAsync(CreateCommentDTO request);
    public Task<List<CommentDTO>> GetCommentsFromPostAsync(int postId);
}