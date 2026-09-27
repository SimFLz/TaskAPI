using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;
using TaskAPI.Entities;
using TaskAPI.Reponses;
using TaskAPI.Requests;
namespace TaskAPI.Services;

public class TaskDbService : ITaskService
{
    private readonly AppDbContext _context;

    
    public TaskDbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskResponse>> GetAllAsync()
    {
        
        return await _context.Tasks.Select(t => new TaskResponse
        {
            Id = t.Id,
            Title = t.Title,
            CategoryId = t.CategoryId,
            CategoryName = t.Category.Name ?? "No Category"
        }).ToListAsync();
    }

    public async Task<TaskResponse?> GetByIdAsync(int id)
    {
        // O EF Core busca pelo ID de forma ultra rápida
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return null;

        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            CategoryId = task.CategoryId
        };
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
    {
        var entityTask = new TaskEntitie
        {
            // 🌟 ATENÇÃO: Não precisamos mais calcular o ID na mão! 
            // O banco de dados vai gerar o ID 1, 2, 3 automaticamente para nós.
            Title = request.Title,
            CategoryId = request.CategoryId
        };
        

        await _context.Tasks.AddAsync(entityTask); // Prepara para salvar no banco
        await _context.SaveChangesAsync();         // Salva de verdade as alterações

        return new TaskResponse
        {
            Id = entityTask.Id,
            Title = entityTask.Title
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskRequest request)
    {
        var taskToUpdate = await _context.Tasks.FindAsync(id);
        if (taskToUpdate == null) return false;

        taskToUpdate.Title = request.Title;
        taskToUpdate.CategoryId = request.CategoryId;
        await _context.SaveChangesAsync(); // Traduz para o comando UPDATE do SQL
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var taskToDelete = await _context.Tasks.FindAsync(id);
        if (taskToDelete == null) return false;

        _context.Tasks.Remove(taskToDelete); // Prepara para remover
        await _context.SaveChangesAsync();              // Traduz para o comando DELETE do SQL
        return true;
    }

   
}
