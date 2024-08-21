using AutoMapper;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI
{
    public class ApiMappingConfiguration : Profile
    {
        public ApiMappingConfiguration() 
        {
            CreateMap<Todo, TodoOutPut>()
               .ConstructUsing(t => new
               TodoOutPut(t.Id,
               t.Title,
               t.DateStart,
               t.DateEnd));
             
            CreateMap<TodoInPut, Todo>();
             
        }
    }
}
