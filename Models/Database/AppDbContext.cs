using Microsoft.EntityFrameworkCore;

namespace web_04_ef_sqlite.Models.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}