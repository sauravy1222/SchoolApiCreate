﻿using Microsoft.EntityFrameworkCore;
using SMS.Models;
using SMSWEBAPI.Models;

namespace SMSWEBAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAttendance> StudentAttendance { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherAttendance> TeacherAttendances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> teachers { get; set; }
    }
}
