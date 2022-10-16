using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolDraftWebsite.Data.Entities;
using SchoolDraftWebsite.Data.SeedData;
using SchoolDraftWebsite.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SchoolDraftWebsite.Data
{
    public class SchoolContext : DbContext
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<FeedingInfoItem> FeedingInfoItems { get; set; }
        public DbSet<FeedingExpense> FeedingExpenses { get; set; }
        public DbSet<ClassFeeInfoItem> ClassFeeInfoItems { get; set; }
        public DbSet<InventoryType> InventoryTypes { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<BookSale> BookSales { get; set; }
        public DbSet<UniformSale> UniformSales { get; set; }
        public DbSet<PandCUniformSale> PandCUniformSales { get; set; }

        public SchoolContext(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? _config["ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var contentRootPath = _env.ContentRootPath;

            var classes = SeedHelper.GetClasses(contentRootPath);
            var students = SeedHelper.GetStudents(contentRootPath);
            var expenseTypes = SeedHelper.GetExpenseTypes(contentRootPath);
            var inventoryTypes = SeedHelper.GetInventoryTypes(contentRootPath);
            var inventoryItems = SeedHelper.GetInventoryItems(contentRootPath);

            foreach (var seedClass in classes)
            {
                modelBuilder.Entity<Class>()
                    .HasData(seedClass);
            }

            foreach (var seedStudent in students)
            {
                modelBuilder.Entity<Student>()
                    .HasData(seedStudent);
            }

            foreach (var seedExpenseType in expenseTypes)
            {
                modelBuilder.Entity<ExpenseType>()
                    .HasData(seedExpenseType);
            }

            foreach (var seedInventoryType in inventoryTypes)
            {
                modelBuilder.Entity<InventoryType>()
                    .HasData(seedInventoryType);
            }

            foreach (var seedInventoryItem in inventoryItems)
            {
                modelBuilder.Entity<InventoryItem>()
                    .HasData(seedInventoryItem);
            }
        }

        public string GetKey()
        {
            var envKey = Environment.GetEnvironmentVariable("Environment");
            var keyPrefix = "";

            switch (envKey)
            {
                case "Development":
                    keyPrefix = "dev";
                    break;
                case "Staging":
                    keyPrefix = "staging";
                    break;
                default:
                    keyPrefix = "dev";
                    break;
            }


            var keyName = $"{keyPrefix}-connection-string";
           // var connectionString = new SecretProvider(_config).GetSecret(keyName);


            return keyName;

            //return Environment.GetEnvironmentVariable("Environment");
        }
    }
}
