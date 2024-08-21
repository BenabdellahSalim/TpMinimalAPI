﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI.Services
{
    public class EfcoreTodoService : ITodoService
    {
        private ApiDbContext context;
        private IMapper mapper;

        public EfcoreTodoService( ApiDbContext context, IMapper mapper ) 
        {
            this.context = context;
            this.mapper = mapper; 
        }
       
        
        public async  Task<TodoOutPut> Add(TodoInPut todo)
        {
            var dbTodo = mapper.Map<Todo>(todo);
            await context.SaveChangesAsync();

            return mapper.Map<TodoOutPut>(dbTodo); 

        }

        public async Task<bool> Delete(int id)
        {
            return await context.TodoDbset.Where(t => t.Id == id).ExecuteDeleteAsync() > 0; 
        }

        public async Task<List<TodoOutPut>> GetAll()
        {
            return (await context.TodoDbset.ToListAsync()).ConvertAll(mapper.Map<TodoOutPut>);
        }

        public async Task<TodoOutPut?> GetById(int id)
        {
            var dbTodo = await context.TodoDbset.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (dbTodo is not null) return mapper.Map<TodoOutPut>(dbTodo);
            return null;
        }

        public async  Task<bool> Update(int id, TodoInPut todo)
        {
            return await context.TodoDbset
                 .Where(t => t.Id == id)
                 .ExecuteUpdateAsync(
                 to =>
                 to.SetProperty(te => te.Title, todo.Title)
                    .SetProperty(te => te.DateStart, todo.DateStart)
                 .SetProperty(te => te.DateEnd, todo.DateEnd)) > 0;
        }
    }
}
