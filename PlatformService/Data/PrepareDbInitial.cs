using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepareDbInitial
    {
        public static void PrePoulateData(IApplicationBuilder application)
        {

            using (var serviceRepo = application.ApplicationServices.CreateScope())
            {
                SeedData(serviceRepo.ServiceProvider.GetService<PlatformAppDbContext>());
            }

        }

        private static void SeedData(PlatformAppDbContext platformAppDbContext)
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
