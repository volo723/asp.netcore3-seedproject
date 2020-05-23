using Microsoft.EntityFrameworkCore;

namespace Attendance.Models
{
    public class CategoryContext : DbContext
    {        
        public CategoryContext(DbContextOptions<CategoryContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
    }
}
