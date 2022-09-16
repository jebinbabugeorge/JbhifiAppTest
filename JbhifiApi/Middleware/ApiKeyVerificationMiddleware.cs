using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JbhifiApi.Middleware
{
    public class ApiKeyVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYHEADER = "api-key";

        public ApiKeyVerificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYHEADER, out var apiKey))
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Api Key was not provided.");

                return;
            }

            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKeys = configuration.GetSection("UserApiKeys").Get<string[]>();

            if (!apiKeys.Contains<string>(apiKey))
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Invalid Api Key.");

                return;
            }

            await _next(context);
        }
    }

    public static class ApiKeyVerificationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyVerification(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyVerificationMiddleware>();
        }
    }
}
