using TaskAPI.Reponses;

namespace TaskAPI.Responses;

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TaskNameResponse> Tasks { get; set; } = new();
}
