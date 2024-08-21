using Microsoft.EntityFrameworkCore;


namespace TpMinimalAPI.Data.Models
{
    public class ApiDbContext : DbContext
    {
       public DbSet<Todo> TodoDbset { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(c =>
            {
               c.ToTable("Todo");
               c.Property(t => t.Title).HasMaxLength(256);
            });
        }
    }
}
