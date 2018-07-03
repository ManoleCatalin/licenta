using Business.Repository;
using Microsoft.EntityFrameworkCore;

using Persistence;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Seeder
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Directory.GetParent(basePath).FullName;

            var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();
            var serviceBuilder = new DbContextOptionsBuilder<DbService>();
            serviceBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            var services = new ServiceCollection();

            services.AddDbContext<DbService>(
            options =>
            {
                options.UseSqlServer("Data Source=DESKTOP-LHEDB6C\\SQLEXPRESS;Initial Catalog=Dist;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            });

            services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<DbService>()
                .AddDefaultTokenProviders();

            var provider = services.BuildServiceProvider();

            var userService = provider.GetService<UserManager<User>>();

            using (var service = new DbService(serviceBuilder.Options))
            {
                IUnitOfWork unitOfWork = new UnitOfWork(service);
                var seeder = new DatabaseSeeder(unitOfWork, service, userService);
                seeder.Seed();
                Console.WriteLine("interests = {0}", unitOfWork.Interests.Count());
            }
        }
    }
}
