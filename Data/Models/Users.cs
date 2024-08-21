namespace TpMinimalAPI.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
