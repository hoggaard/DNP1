namespace Entities;

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public DateTime Created { get; set; }
    
    public int UserId { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Body: {Body}, Dislikes: {Dislikes}, Likes: {Likes}, Created: {Created}";
    }
}