﻿using System;
using AutoMapper;
using MedicalJournals.Common.Settings;
using MedicalJournals.Entities;
using MedicalJournals.Helpers;
using MedicalJournals.Identity;
using MedicalJournals.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MedicalJournals.Entities.Extensions;
using MedicalJournals.Models.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MedicalJournals.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add session related services.
            services.AddSession();

            // Add framework services.
            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                // .Addcontext<JournalContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]))
                .AddDbContext<JournalContext>(o=>o.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], b =>
                {
                    var assemblyName = typeof(JournalContext).GetAssembly().GetName().Name;

                    b.MigrationsAssembly(assemblyName);
                }));

            services.AddIdentity<ApplicationUser, JournalRole>(o => {
                    o.Password.RequiredLength = 6;
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<JournalContext, Guid>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, JournalRole, JournalContext, Guid>>()
                .AddRoleStore<RoleStore<JournalRole, JournalContext, Guid>>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceScopeFactory scopeFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Configure Session.
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            Mapper.Initialize(config =>
            {
                var mapperConfig = new AutoMapperConfig();
                mapperConfig.Execute(config);
            });

            Mapper.AssertConfigurationIsValid();


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            scopeFactory.SeedData(loggerFactory.CreateLogger("Seeding data"));

            app.UseStaticFiles();
            app.UseIdentity();

            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}");
            });
        }
    }
}
