using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<RequestTimeMiddleware>();

            app.Use(async (context, next) =>
            {
                var timer = new Stopwatch();
                timer.Start();
                timer.Stop();
                await next.Invoke();
                await context.Response.WriteAsync($"Middleware function time: {timer.ElapsedMilliseconds / 1000.0}\n");
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("done\n");
            });
        }
    }
}
