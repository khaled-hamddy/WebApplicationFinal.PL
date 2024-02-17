using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.DAL.Data
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>/* <IdentityUser, IdentityRole,string> by default*/
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            //instead of waiting paramaterless constructor to chain
            //on parameterized one i put the parameterized directly
            //and will pass the connections string with it in program
            //so i dont need to override  onconfiguring any more i will use original one
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server = .; Database= MvcWebApp; Trusted_connection = True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);//to get fluent api done for Identity in onmodel because overriding hides them (which is priamry key attr)
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles"); to change name aspnetroles column
                //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
                //it will go to configuration class and apply all of them
            }
            public DbSet<Department> Departments { get; set; }
            public DbSet<Employee> Employees { get; set; }
        ///Identity Dbsets(4 Dbsets to Users and 3 Dbsets = 7 Dbsets) inherited from IdentityDbContext 
        ///=> Generate 7 Tables and including tables(Users + Roles + Relationship between them(UserRoles) )
        ///public DbSet<IdentityUser> Users { get; set; } we replaced it with inheriting them from IdentityDbContext instead of inheriting  dbcontext only
        ///public DbSet<IdentityRole> Roles { get; set; }

    }

}
