using Microsoft.EntityFrameworkCore;
using StudentPortal2.Models.Entities;

namespace StudentPortal2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
