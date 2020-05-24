using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Models
{
    public class CategoryContext : DbContext
    {
        public CategoryContext (DbContextOptions<CategoryContext> options)
            : base(options)
        {
        }

        
    }
}
