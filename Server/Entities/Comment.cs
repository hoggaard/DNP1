namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public DateTime Created { get; set; }
    
    public int PostId { get; set; }
}