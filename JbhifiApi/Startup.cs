using AspNetCoreRateLimit;
using JbhifiApi.Middleware;
using JbhifiApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JbhifiApi
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
            services.AddOptions();

            services.AddMemoryCache();

            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));

            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));

            services.AddInMemoryRateLimiting();

            services.AddCors();

            services.AddMvc();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddControllers();

            services.AddHttpClient("OpenWeatherMap", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration.GetValue<string>("OpenWeatherMap:BaseAddress"));
            });

            services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApiKeyVerification();

            app.UseClientRateLimiting();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
