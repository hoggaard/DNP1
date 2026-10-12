namespace APIContracts;

public class UpdateCommentDTO
{
    public required string Body { get; set; }
    public DateTime Created { get; set; }
    public int Id { get; set; }
}