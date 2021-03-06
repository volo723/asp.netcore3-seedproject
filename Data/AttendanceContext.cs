﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Models
{
    public class AttendanceContext : DbContext
    {
        public AttendanceContext (DbContextOptions<AttendanceContext> options)
            : base(options)
        {
        }

        public DbSet<Attendance.Models.QuestionPool> QuestionPoolNew { get; set; }
        public DbSet<Attendance.Models.Category> Category { get; set; }
        public DbSet<Attendance.Models.VIQInfoModel> VIQInfo { get; set; }
    }
}
