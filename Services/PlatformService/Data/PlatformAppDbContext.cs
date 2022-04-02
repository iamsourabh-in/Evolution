using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformAppDbContext : DbContext
    {
        public PlatformAppDbContext(DbContextOptions<PlatformAppDbContext> options) : base(options)
        {

        
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
