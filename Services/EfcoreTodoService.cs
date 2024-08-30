using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using TpMinimalAPI.Data;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI.Services
{
    public class EfcoreTodoService : ITodoService
    {
        private ApiDbContext context;
        private IMapper mapper;

        public EfcoreTodoService(ApiDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<TodoOutPut> Add(TodoInPut todo, int UsersId)
        {
            var dbTodo = mapper.Map<Todo>(todo);
            dbTodo.UsersId = UsersId;
            context.TodoDbset.Add(dbTodo);
            await context.SaveChangesAsync();

            return mapper.Map<TodoOutPut>(dbTodo);

        }

        public async Task<bool> Delete(int id, int UsersId)
        {
            return await context.TodoDbset.Where(t => t.Id == id && t.UsersId == UsersId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<TodoOutPut>> GetActive(int UsersId)
        {
            var result = (await context.TodoDbset.Where(t => t.UsersId == UsersId &&  (t.DateEnd > DateTime.Now || t.DateEnd == null)).ToListAsync()).ConvertAll(mapper.Map<TodoOutPut>);
            return result;
        }

        public async Task<List<TodoOutPut>> GetAll(int UsersId)
        {
            return (await context.TodoDbset.Where(t => t.UsersId == UsersId).ToListAsync()).ConvertAll(mapper.Map<TodoOutPut>);
        }

        public async Task<TodoOutPut?> GetById(int id, int UsersId)
        {
            var dbTodo = await context.TodoDbset.Where(t => t.Id == id && t.UsersId == UsersId).FirstOrDefaultAsync();
            if (dbTodo is not null) return mapper.Map<TodoOutPut>(dbTodo);
            return null;
        }

        public async Task<bool> Update(int id, int UsersId, TodoInPut todo)
        {
            return await context.TodoDbset
                 .Where(t => t.Id == id && t.UsersId == UsersId)
                 .ExecuteUpdateAsync(
                 to =>
                 to.SetProperty(te => te.Title, todo.Title)
                    .SetProperty(te => te.DateStart, todo.DateStart )
                 .SetProperty(te => te.DateEnd, todo.DateEnd)) > 0;
        }


    }
}
