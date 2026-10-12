namespace APIContracts;

public class UpdatePostDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public DateTime Created { get; set; }
}