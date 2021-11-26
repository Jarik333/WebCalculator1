
using Microsoft.EntityFrameworkCore;
namespace EFDataApp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
          
            Database.EnsureCreated();
        }
        
    }
}
