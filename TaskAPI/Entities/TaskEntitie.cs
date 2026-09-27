namespace TaskAPI.Entities;

public class TaskEntitie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? CategoryId { get; set; }
    public CategoryEntitie? Category { get; set; }
}
        