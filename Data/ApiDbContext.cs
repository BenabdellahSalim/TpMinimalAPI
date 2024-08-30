using Microsoft.EntityFrameworkCore;
using TpMinimalAPI.Data.Models;


namespace TpMinimalAPI.Data
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
                c.HasKey(t => t.Id);

                c.Property(t => t.Title).HasMaxLength(256);
                c.HasOne(o => o.Users).WithMany(u => u.Todos).HasForeignKey(u => u.UsersId);
            });
            modelBuilder.Entity<Users>(u =>
            {
                u.ToTable("Users");
                u.HasKey(t => t.Id);

                u.Property(t => t.Token).IsUnicode().HasMaxLength(6);
                u.Property(t => t.Name).HasMaxLength(256);
                u.HasMany(o => o.Todos).WithOne(o => o.Users).HasForeignKey(o => o.UsersId);

                u.HasIndex(u => u.Token).IsUnique();
            });

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("filename=api.db");

        //}
    }
}
