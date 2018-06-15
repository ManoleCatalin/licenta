using Business.Repository;
using Microsoft.EntityFrameworkCore;

using Persistence;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Core.Interfaces;

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


            using (var service = new DbService(serviceBuilder.Options))
            {
                IUnitOfWork unitOfWork = new UnitOfWork(service);
                var seeder = new DatabaseSeeder(unitOfWork);
                seeder.Seed();
                Console.WriteLine("interests = {0}", unitOfWork.Interests.Count());
            }
        }
    }
}
