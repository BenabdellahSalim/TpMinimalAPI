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
               TodoOutPut(t.UsersId,
               t.Title,
               t.DateStart,
               t.DateEnd));
            CreateMap<Users, UsersOutPut>()
              .ConstructUsing(t => new
              UsersOutPut(t.Id,
              t.Name,
              t.Token));
            //int Id, string Name, string Token
            CreateMap<TodoInPut, Todo>();
            CreateMap<UsersInPut, Users>();

        }
       
    }
}
