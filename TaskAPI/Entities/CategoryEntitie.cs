namespace TaskAPI.Entities;

public class CategoryEntitie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TaskEntitie> Tasks { get; set; } = new();
}
