using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Requests;

public class UpdateTaskRequest
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    [Required]
    public int? CategoryId { get; set; }
}
