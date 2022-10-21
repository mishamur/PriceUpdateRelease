using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DbApi
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public string DbPath { get; }
        public ApplicationContext()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(folder, "PriceData");

            Directory.CreateDirectory(path);

            this.DbPath = Path.Combine(path, "products.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //первичный ключ-позиция товара
            modelBuilder.Entity<Product>().HasKey(product => new { product.Position });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //forPostgres
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProductsDb;Username=postgres;Password=yourPassword");
            //forSqlite
            optionsBuilder.UseSqlite($"Data Source={this.DbPath}");
        }
    }
}
