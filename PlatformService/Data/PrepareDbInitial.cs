using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace PlatformService.Data
{
    public static class PrepareDbInitial
    {
        public static void PrePoulateData(IApplicationBuilder application, IWebHostEnvironment env)
        {

            using (var serviceRepo = application.ApplicationServices.CreateScope())
            {
                SeedData(serviceRepo.ServiceProvider.GetService<PlatformAppDbContext>(),env);
            }

        }

        private static void SeedData(PlatformAppDbContext platformAppDbContext, IWebHostEnvironment env)
        {
            if(env.IsProduction())
            {
                System.Console.WriteLine("Applying Migrations");
                platformAppDbContext.Database.Migrate();
            }
            else
            {

            if (!platformAppDbContext.Platforms.Any())
            {
                Console.WriteLine("Seeding Data.....");
                platformAppDbContext.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "Dotnet",
                        Publisher = "Microsoft",
                        Cost = "Free",
                    },
                    new Platform()
                    {
                        Name = "SQL Server",
                        Publisher = "Microsoft",
                        Cost = "Free",
                    },
                    new Platform()
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free",
                    }
                );
                platformAppDbContext.SaveChanges();

            }
            else
            {
                Console.WriteLine("Data already Present");
            }
            }

        }

    }
}
