namespace TpMinimalAPI.DTO
{
    public record TodoInPut
    {
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; } 
    }
}
