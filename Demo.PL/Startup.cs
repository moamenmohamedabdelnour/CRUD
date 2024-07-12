using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			//Ya CLR lw 7d talab mnk Object mn Class bi Implement IDepartmentRepsitory e3mlo object mn class el DepartmentRepoSitory
			//services.AddScoped<IDepartmentRepository,DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository,EmployeeRepository>();
			services.AddScoped<IUnitOfWork,UnitOfWork>();
			
			services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));


			services.AddIdentity<ApplicationUser, IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
			services.ConfigureApplicationCookie(config =>
			{
				config.LoginPath = "/Account/SignIn";
			});
			//services.AddAuthorization();
			//services.AddAuthorization();
			//services.AddAuthentication("Cookies");
					//.AddCookie("hamda", config =>
					//{
					//	config.LoginPath = "/Account/SignIn";
					//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
