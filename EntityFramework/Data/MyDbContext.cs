using EntityFramework.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<SaleData> SalesData { get; set; }

        public MyDbContext(DbContextOptions options) : base(options){}

        public DbSet<T> GetTableByType<T>(T model) where T : class
        {
            return model switch
            {
                Product => Products as DbSet<T>,
                Buyer => Buyers as DbSet<T>,
                Sale => Sales as DbSet<T>,
                ProvidedProduct => ProvidedProducts as DbSet<T>,
                SaleData => SalesData as DbSet<T>,
                SalesPoint => SalesPoints as DbSet<T>,
                _ => null,
            };
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public void AddNSave<T>(T model) where T : class
        {
            GetTableByType(model).Add(model);
            SaveChanges();
        }

        public void UpdateNSave<T>(T model) where T : class
        {
            GetTableByType(model).Update(model);
            SaveChanges();
        }

        public void DeleteNSave<T>(T model) where T : class
        {
            GetTableByType(model).Remove(model);
            SaveChanges();
        }

        public Sale GetSale(int id) => Sales.Find(id);

        public SalesPoint GetSalesPoint(int id) => SalesPoints.Find(id);
        
        public Buyer GetBuyer (int id) => Buyers.Find(id);
    }
}