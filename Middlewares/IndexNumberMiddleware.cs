using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1APBD.Middlewares
{
    public class IndexNumberMiddleware
    {
        private readonly RequestDelegate _next;

        public IndexNumberMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("IndexNumber", "18398");
            await _next(httpContext);
            
       }
    }
}
