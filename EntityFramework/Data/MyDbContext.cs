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
        private Random random = new();
        private const int quantity = 50;

        public const string conectionString = @"Data Source = (localdb)\MSSQLSERVER_DEV;Initial Catalog = StoreDB;";
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<SaleData> SalesData { get; set; }

        public MyDbContext(DbContextOptions options) : base(options)
        {
            LoadProducts();
            LoadProvidedProducts();
            LoadSalesData();
            LoadSalesPoints();
            LoadSales();
            LoadBuyers();
        }

        public DbSet<T> GetByType<T>(T model) where T : class
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

        public void AddNSave<T>(T model) where T : class
        {
            GetByType(model).Add(model);
            SaveChanges();
        }

        public void UpdateNSave<T>(T model) where T : class
        {
            GetByType(model).Update(model);
            SaveChanges();
        }

        public void DeleteNSave<T>(T model) where T : class
        {
            GetByType(model).Remove(model);
            SaveChanges();
        }

        public Sale GetSale(int id) => Sales.Find(id);

        public SalesPoint GetSalesPoint(int id) => SalesPoints.Find(id);
        
        public Buyer GetBuyer (int id) => Buyers.Find(id);

        private void Load(Action action)
        {
            for (int i = 0; i < quantity; i++)
                action();
            SaveChanges();
        }

        private void Detach(object item)
        {
            Entry(item).State = EntityState.Detached;
        }

        private void LoadProducts()
        {
            Load(() =>
            {
                var product = new Product($"Product {random.Next(1000)}", random.Next(1000));
                Products.Add(product);
                Detach(product);
            });
        }

        private void LoadProvidedProducts()
        {
            Load(() =>
            {
                Product product = Products.Find(random.Next(quantity));
                var item = new ProvidedProduct(product, random.Next(1000));
                ProvidedProducts.Add(item);
                Detach(item);
            });
        }

        private void LoadSalesData()
        {
            Load(() =>
            {
                Product product = Products.Find(random.Next(quantity));
                var item = new SaleData(product, random.Next(1000), random.Next(10000));
                SalesData.Add(item);
                Detach(item);
            });
        }

        private void LoadSalesPoints()
        {
            Load(() =>
            {
                var providedProducts = GetRandomProvidedProducts();
                var item = new SalesPoint($"SalesPoint {random.Next(1000)}", providedProducts);
                SalesPoints.Add(item);
                Detach(item);
            });
        }

        private void LoadSales()
        {
            Load(() => {
                Buyer buyer = Buyers.Find(random.Next(quantity));
                SalesPoint salesPoint = SalesPoints.Find(random.Next(quantity));
                var item = new Sale(DateTime.Today, DateTime.Now, salesPoint, buyer, GetRandomSalesData(), random.Next(10000));
                Sales.Add(item);
                Detach(item);
            });
        }

        private void LoadBuyers()
        {
            Load(() =>
            {
                var item = new Buyer($"Buyer {random.Next(1000)}", GetRandomSales());
                Buyers.Add(item);
                //Detach(item);
            });
        }

        private List<ProvidedProduct> GetRandomProvidedProducts()
        {
            List<ProvidedProduct> products = new();
            int quan = random.Next(quantity);
            for (int i = 0; i < quan; i++)
            {
                var item = ProvidedProducts.Find(i);
                if (item != null)
                    products.Add(item);
            }
            return products;
        }

        private List<SaleData> GetRandomSalesData()
        {
            List<SaleData> sales = new();

            int quan = random.Next(quantity);
            for (int i = 0; i < quan; i++)
            {
                var item = SalesData.Find(i);
                if (item != null)
                    sales.Add(item);
            }
            return sales;
        }

        private List<Sale> GetRandomSales()
        {
            List<Sale> sales = new();

            int quan = random.Next(quantity);
            for (int i = 0; i < quan; i++)
            {
                var item = Sales.Find(i);
                if (item != null)
                    sales.Add(item);
            }
            return sales;
        }

        private Product getRandomProduct() => Products.Find(random.Next(quantity));

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(conectionString);
        //}
    }
}