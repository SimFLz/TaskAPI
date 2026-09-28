using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Reponses;
using TaskAPI.Requests;
using TaskAPI.Services;
namespace TaskAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<TaskResponse> tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null)
        {
            return NotFound("Task not Found!");
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var newTask = await _taskService.CreateAsync(request);
        return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskRequest request)
    {
        var sucess = await _taskService.UpdateAsync(id, request);
        if(!sucess)
        {
            return NotFound("Task not Found!");
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var success = await _taskService.DeleteAsync(id);
        if (!success) return NotFound("Task not Found to delete!");
        return NoContent();
    }
}
