using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MainProjectFull.DAL;
using Microsoft.AspNetCore.Identity;
using MainProjectFull.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MainProjectFull
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CodeContext>(options => options.UseSqlServer(@"Data Source=.; Initial Catalog=CodeDB; Integrated Security=True"));

            services.AddIdentity<Users, Role>()
                .AddEntityFrameworkStores<CodeContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<Users, Role, CodeContext, int>>()
                .AddRoleStore<RoleStore<Role, CodeContext, int>>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();


            app.UseAuthentication();

     

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
