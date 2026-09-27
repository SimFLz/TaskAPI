using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Requests;

public class CreateCategoryRequest
{
    [Required]
    public string CategoryName { get; set; } = string.Empty;

    public int CategoryId { get; set; }
}
