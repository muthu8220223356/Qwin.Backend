using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Qwin.WebAPI.Data
{
    public class QwinDbContext:DbContext
    {
        public QwinDbContext(DbContextOptions<QwinDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
