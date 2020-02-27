using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DashboardApi.Repositories;
using AutoMapper;

namespace DashboardApi
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
            // add cors for dev env
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // TODO: Try Postgres
            services.AddDbContextPool<DashboardContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAutoMapper(typeof(Startup));

            //services.AddSingleton<ICustomerRepository, MockCustomerRepository>();
            services.AddScoped<ICustomerRepository, SQLCustomerRepository>();
            services.AddScoped<IOrderRepository, SQLOrderRepository>();
            services.AddScoped<IServerRepository, SQLServerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }

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
