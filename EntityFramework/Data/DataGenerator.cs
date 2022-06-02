using EntityFramework.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data
{
    public class DataGenerator
    {
        private Random random = new();
        private const int quantity = 50;
        private MyDbContext context;

        public DataGenerator(MyDbContext context)
        {
            this.context = context;
        }

        public void Run()
        {
            LoadProducts();
            LoadProvidedProducts();
            LoadSalesData();
            LoadSalesPoints();
            LoadSales();
            LoadBuyers();
        }

        private void Load(Action action)
        {
            for (int i = 0; i < quantity; i++)
                action();
            context.SaveChanges();
        }

        private void Detach(object item)
        {
            //context.Entry(item).State = EntityState.Added;
        }

        private void LoadProducts()
        {
            Load(() =>
            {
                var product = new Product($"Product {random.Next(1000)}", random.Next(1000));
                context.Products.Add(product);
                Detach(product);
            });
        }

        private void LoadProvidedProducts()
        {
            Load(() =>
            {
                Product product = getRandomProduct();
                var item = new ProvidedProduct(product, random.Next(1000));
                context.ProvidedProducts.Add(item);
                Detach(item);
            });
        }

        private void LoadSalesData()
        {
            Load(() =>
            {
                Product product = getRandomProduct();
                var item = new SaleData(product, random.Next(1000), random.Next(10000));
                context.SalesData.Add(item);
                Detach(item);
            });
        }

        private void LoadSalesPoints()
        {
            Load(() =>
            {
                var providedProducts = GetRandomProvidedProducts();
                var item = new SalesPoint($"SalesPoint {random.Next(1000)}", providedProducts);
                context.SalesPoints.Add(item);
                Detach(item);
            });
        }

        private void LoadSales()
        {
            Load(() => {
                Buyer buyer = context.Buyers.Find(random.Next(quantity));
                SalesPoint salesPoint = context.SalesPoints.Find(random.Next(quantity));
                var item = new Sale(DateTime.Today, DateTime.Now, salesPoint, buyer, GetRandomSalesData(), random.Next(10000));
                context.Sales.Add(item);
                Detach(item);
            });
        }

        private void LoadBuyers()
        {
            Load(() =>
            {
                var item = new Buyer($"Buyer {random.Next(1000)}", GetRandomSales());
                context.Buyers.Add(item);
                //context.Entry<Buyer>(item).State = EntityState.Added;
                //Detach(item);
            });
        }

        private List<ProvidedProduct> GetRandomProvidedProducts()
        {
            List<ProvidedProduct> products = new();
            int quan = random.Next(quantity);
            for (int i = 0; i < quan; i++)
            {
                var item = context.ProvidedProducts.Find(i);
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
                var item = context.SalesData.Find(i);
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
                var item = context.Sales.Find(i);
                if (item != null)
                    sales.Add(item);
            }
            return sales;
        }

        private Product getRandomProduct() => context.Products.Find(random.Next(quantity));
    }
}
