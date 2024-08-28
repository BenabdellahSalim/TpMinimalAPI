namespace TpMinimalAPI.Data.Models
{
    public class Todo
    {

        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime DateStart { get; set; } = DateTime.Now; 
        public DateTime? DateEnd { get; set; } = null;
        public Users Users { get; set; }
        public int UsersId { get; set; }
    }
}
