using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;
using TaskAPI.Entities;
using TaskAPI.Reponses;
using TaskAPI.Requests;
using TaskAPI.Responses;
namespace TaskAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Tasks)
            .ToListAsync();

        if (categories == null) return null;

        return categories.Select(categories => new CategoryResponse
        {
            Id = categories.Id,
            Name = categories.Name,
            Tasks = categories.Tasks.Select(task => new TaskNameResponse
            {
                Title = task.Title
            }).ToList()
        }).ToList();
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return null;

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Tasks = category.Tasks.Select(task => new TaskNameResponse
            {
                Title = task.Title
            }).ToList()
        };
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var category = new CategoryEntitie
        {
            Name = request.CategoryName
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;
        category.Name = request.CategoryName;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}

