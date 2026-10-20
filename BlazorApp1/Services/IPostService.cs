using APIContracts;

namespace BlazorApp1.Services;

public interface IPostService
{
    public Task<PostDTO> AddPostAsync(CreatePostDTO request);
    public  Task<List<PostDTO>> GetAllPostsAsync();
    public Task<PostDTO> GetPostAsync(int id);
}