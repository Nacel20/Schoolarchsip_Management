using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Filiere> Filieres { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Registration> Registrations { get; set; }

    }
}
