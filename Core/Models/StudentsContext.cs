using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Models
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentsContext(DbContextOptions<StudentsContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.Faculty)
                .IsRequired();
        }
    }
}
