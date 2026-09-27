using TaskAPI.Entities;
using TaskAPI.Reponses;
using TaskAPI.Requests;
using TaskAPI.Responses;

namespace TaskAPI.Services;

public interface ICategoryService
{
   Task<List<CategoryResponse>> GetAllAsync();
   Task<CategoryResponse?> GetByIdAsync(int id);
   Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
   Task<bool> UpdateAsync(int id, UpdateCategoryRequest request);
   Task<bool> DeleteAsync(int id);
}
