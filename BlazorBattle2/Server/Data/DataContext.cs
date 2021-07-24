using BlazorBattle2.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle2.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Unit> Units { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        
    }
}