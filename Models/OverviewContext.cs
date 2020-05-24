using Microsoft.EntityFrameworkCore;

namespace Attendance.Models
{
    public class OverviewContext : DbContext
    {        
        public OverviewContext(DbContextOptions<OverviewContext> options)
            : base(options)
        {

        }

       
    }
}
