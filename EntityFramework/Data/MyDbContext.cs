using EntityFramework.Data.Models;
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
        private struct TokenOf<X> { };

        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<SaleData> SalesData { get; set; }

        public MyDbContext(DbContextOptions options) : base(options){}

        public DbSet<T> GetTable<T>(T model) where T : class =>
            model switch
            {
                Product => Products as DbSet<T>,
                Buyer => Buyers as DbSet<T>,
                Sale => Sales as DbSet<T>,
                ProvidedProduct => ProvidedProducts as DbSet<T>,
                SaleData => SalesData as DbSet<T>,
                SalesPoint => SalesPoints as DbSet<T>,
                _ => null,
            };


        public DbSet<T> GetTable<T>() where T : class =>
            default(TokenOf<T>) switch
            {
                TokenOf<Product> => Products as DbSet<T>,
                TokenOf<Buyer> => Buyers as DbSet<T>,
                TokenOf<Sale> => Sales as DbSet<T>,
                TokenOf<ProvidedProduct> => ProvidedProducts as DbSet<T>,
                TokenOf<SaleData> => SalesData as DbSet<T>,
                TokenOf<SalesPoint> => SalesPoints as DbSet<T>,
                _ => null,
            };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public void AddNSave<T>(T model) where T : class
        {
            GetTable<T>().Add(model);
            SaveChanges();
        }

        public void UpdateNSave<T>(T model) where T : class
        {
            GetTable<T>().Update(model);
            SaveChanges();
        }

        public void DeleteNSave<T>(T model) where T : class
        {
            GetTable<T>().Remove(model);
            SaveChanges();
        }

        public Sale GetSale(int id) => Sales.Find(id);

        public SalesPoint GetSalesPoint(int id) => SalesPoints.Find(id);
        
        public Buyer GetBuyer (int id) => Buyers.Find(id);
    }
}