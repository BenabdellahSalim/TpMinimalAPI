using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI.Services
{
    public interface ITodoService
    {
        Task<List<TodoOutPut>> GetAll( int UsersId);
        Task<List<TodoOutPut>> GetActive(int UsersId);
        Task<TodoOutPut?> GetById(int id, int UsersId);
        Task<TodoOutPut> Add(TodoInPut todo, int UsersId);
        Task<bool> Update(int id,int UsersId ,TodoInPut item);
        Task<bool> Delete(int id, int UsersId);
    }
}
