using System;
using System.Diagnostics;
using System.Threading;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StudentsContext>(options =>
                options.UseSqlServer(connection));
            services.AddTransient<IStudentService, StudentService>();
            services.Configure<Author>(Configuration);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            
            app.UseMiddleware<RequestTimeMiddleware>();

            app.Use(async (context, next) =>
            {
                var timer = new Stopwatch();
                timer.Start();
                //Do something
                timer.Stop();
                await next.Invoke();
                await context.Response.WriteAsync($"Middleware function time: {timer.ElapsedMilliseconds / 1000.0}<br/>");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
