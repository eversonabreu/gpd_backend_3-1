using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GPD.Backend.Api
{
    public class HandlingMiddleware
    {
        private readonly RequestDelegate next;

        public HandlingMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exc)
            {
                var innerException = exc.InnerException ?? exc;
                Console.Out.WriteLine(innerException.Message, innerException.StackTrace);
                await WriteExceptionAsync(context, innerException);
            }
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = 500;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message = exception.Message,
                    exception = exception.GetType().Name
                }
            })).ConfigureAwait(false);
        }
    }
}
