﻿using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
	internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(D => D.Id).UseIdentityColumn(10, 10);
			builder.HasMany(D=>D.Employees).WithOne(E=>E.Department).OnDelete(DeleteBehavior.Cascade);
			builder.Property(D => D.Name).IsRequired(true);
			builder.Property(D => D.Code).IsRequired(true);
		}
	}
}
