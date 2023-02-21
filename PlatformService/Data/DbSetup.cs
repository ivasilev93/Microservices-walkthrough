using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DbSetup
    {
        public static void Init(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetRequiredService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platorms.Any())
            {
                context.Platorms.AddRange(new[]
                {
                    new Platform {
                        Name = "Platform 1",
                        Cost = 1000,
                        Publisher = "MSDN"
                    },
                    new Platform {
                        Name = "Platform 2",
                        Cost = 2500,
                        Publisher = "Apple"
                    }
                });

                context.SaveChanges();
            }

            Console.WriteLine(context.Platorms.Count());
        }
    }
}
