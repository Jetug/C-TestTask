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
        private Random random = new Random();
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


        public Sale GetSale(int id) => Sales.Find(id);

        public SalesPoint GetSalesPoint(int id) => SalesPoints.Find(id);
        
        public Buyer GetBuyer (int id) => Buyers.Find(id);

        private void LoadProducts()
        {
            Load(() =>
            {
                Products.Add(new Product($"Product {random.Next(1000)}", random.Next(1000)));
            });
        }

        private void LoadProvidedProducts()
        {
            Load(() =>
            {
                Product product = Products.Find(random.Next(quantity));
                ProvidedProducts.Add(new ProvidedProduct(product, random.Next(1000)));
            });
        }

        private void LoadSalesData()
        {
            Load(() =>
            {
                Product product = Products.Find(random.Next(quantity));
                SalesData.Add(new SaleData(product, random.Next(1000), random.Next(10000)));
            });
        }

        private void LoadSalesPoints()
        {
            Load(() =>
            {
                var providedProducts = GetRandomProvidedProducts();
                SalesPoints.Add(new SalesPoint($"SalesPoint {random.Next(1000)}", providedProducts));
            });
        }

        private void LoadSales()
        {
            Load(() => {
                Buyer buyer = Buyers.Find(random.Next(quantity));
                SalesPoint salesPoint = SalesPoints.Find(random.Next(quantity));
                Sales.Add(new Sale(DateTime.Today, DateTime.Now, salesPoint, buyer, GetRandomSalesData(), random.Next(10000)));
            });
        }

        private void LoadBuyers()
        {
            Load(() =>
            {
                Buyers.Add(new Buyer($"Buyer {random.Next(1000)}", GetRandomSales()));
            });
        }

        private void Load(Action action)
        {
            for (int i = 0; i < quantity; i++)
                action();
            SaveChanges();
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