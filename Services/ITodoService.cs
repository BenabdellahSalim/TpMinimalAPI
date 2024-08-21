using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI.Services
{
    public interface ITodoService
    {
        Task<List<TodoOutPut>> GetAll();
        Task<List<TodoOutPut>> GetActive();
        Task<TodoOutPut?> GetById(int id);
        Task<TodoOutPut> Add(TodoInPut todo);
        Task<bool> Update(int id, TodoInPut todo);
        Task<bool> Delete(int id);
    }
}
