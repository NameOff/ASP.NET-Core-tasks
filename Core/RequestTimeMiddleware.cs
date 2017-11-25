using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core
{
    public class RequestTimeMiddleware
    {
        private readonly RequestDelegate next;

        public RequestTimeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var timer = new Stopwatch();
            timer.Start();
            await next.Invoke(context);
            await context.Response.WriteAsync($"Middleware class + function time: {timer.ElapsedMilliseconds / 1000.0}\n");
        }
    }
}
