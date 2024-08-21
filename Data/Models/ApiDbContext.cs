using Microsoft.EntityFrameworkCore;


namespace TpMinimalAPI.Data.Models
{
    public class ApiDbContext : DbContext
    {
       public DbSet<Todo> TodoDbset { get; set; }
        public DbSet<Users> Users { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(c =>
            {
               c.ToTable("Todo");
               c.Property(t => t.Title).HasMaxLength(256);
                c.Property(t => t.DateStart).HasDefaultValue(DateTime.Now);
                c.Property(t => t.DateEnd);
            });
            modelBuilder.Entity<Users>(c =>
            {
                c.ToTable("Users");
                c.Property(t => t.Token).IsUnicode().HasMaxLength(6);
                c.Property(t =>t.Name).HasMaxLength(256);
                 
            });
        }
    }
}
