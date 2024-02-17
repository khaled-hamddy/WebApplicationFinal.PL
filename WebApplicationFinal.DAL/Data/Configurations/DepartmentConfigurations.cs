using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.DAL.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //fluent apis

            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.HasMany(D => D.Employees).WithOne(E => E.Department).IsRequired(false).HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(D => D.code)
                .IsRequired(true);

            builder.Property(E => E.name)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}
