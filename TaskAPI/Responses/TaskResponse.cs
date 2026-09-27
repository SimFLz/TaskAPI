namespace TaskAPI.Reponses;

public class TaskResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }
}
