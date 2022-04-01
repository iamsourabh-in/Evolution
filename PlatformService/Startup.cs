using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlatformService.SyncDataServices;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.Data;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.AsyncDataServices;
using Microsoft.AspNetCore.Http;
using PlatformService.Policies;

namespace PlatformService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                services.AddDbContext<PlatformAppDbContext>(
                    optionsAction: options =>
                        options.UseSqlServer(Configuration.GetConnectionString("PlatformDB"))
                );
            }
            else
            {
                services.AddDbContext<PlatformAppDbContext>(
                    optionsAction: options => options.UseInMemoryDatabase("InMemDB")
                );
            }
            services.AddHttpClient();
            services.AddScoped<IPlatfomRepo, PlatformRepo>();
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            services.AddScoped<IMessageBusClient, MessageBusClient>();
            services.AddSingleton<ClientPolicy>(new ClientPolicy());
            services.AddGrpc();
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo { Title = "PlatformService", Version = "v1" }
                    );
                }
            );

            Console.WriteLine($"{Configuration["CommandService"]} this is used.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1")
                );
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGrpcService<GrpcPlatformService>();
                    endpoints.MapGet("/protos/platform.proto", async context => {
                        await context.Response.WriteAsync(File.ReadAllText("PRotos/platforms.proto"));
                    });
                }
            );

            PrepareDbInitial.PrePoulateData(app, env);
        }
    }
}
