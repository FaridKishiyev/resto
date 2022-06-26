using FrontToBack5.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack5.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        public DbSet<Team> teams { get; set; }
    }
}
