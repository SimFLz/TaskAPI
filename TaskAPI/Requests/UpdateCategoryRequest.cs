using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Requests;

public class UpdateCategoryRequest
{
    [Required]
    public string CategoryName { get; set; } = string.Empty;
}
