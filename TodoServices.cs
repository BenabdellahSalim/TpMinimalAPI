using System.Reflection;

namespace TpMinimalAPI
{
    public class TodoServices
    {
        public List<Todo> TodoList = new List<Todo>
        {
            new("Sport", "2024/08/11" ,"2024/09/11"),
            new("studies", "2024/06/07", null )
        };

        public List<Todo> GetAll() => TodoList;

        public Todo TodoAdd(string title,string dateStart, string dateEnd)
        {
            var ToDo = new Todo(title, dateStart, dateEnd);
            TodoList.Add(ToDo);
            return ToDo;
        }





    }
}
