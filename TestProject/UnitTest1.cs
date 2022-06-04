using EntityFramework.Data;
using EntityFramework.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        private MyDbContext context;
        private DateTime time;
        private DateTime date;

        [TestInitialize]
        public void Setup()
        {
            context = GetDbContext();
            FillDb();
        }

        [TestMethod]
        public void TestDb()
        {
            Assert.AreEqual(1, context.Buyers.Count());
            Assert.AreEqual("test buyer", context.Buyers.First().Name);

            Assert.AreEqual(1, context.Products.Count());
            Assert.AreEqual("test product", context.Products.First().Name);

            Assert.AreEqual(1, context.ProvidedProducts.Count());
            Assert.AreEqual(context.Products.First(), context.ProvidedProducts.First().Product);
            Assert.AreEqual(10, context.ProvidedProducts.First().ProductQuantity);

            Assert.AreEqual(1, context.SalesPoints.Count());
            Assert.AreEqual("test sales point", context.SalesPoints.First().Name);
            Assert.AreEqual(1, context.SalesPoints.First().ProvidedProducts.Count());
            Assert.AreEqual(context.ProvidedProducts.First(), context.SalesPoints.First().ProvidedProducts.First());


            Assert.AreEqual(1, context.SalesData.Count());
            Assert.AreEqual(context.Products.First(), context.SalesData.First().Product);
            Assert.AreEqual(10, context.SalesData.First().ProductQuantity);
            Assert.AreEqual(200, context.SalesData.First().ProductIdAmount);

            Assert.AreEqual(1, context.Sales.Count());
            Assert.AreEqual(date, context.Sales.First().Date);
            Assert.AreEqual(time, context.Sales.First().Time);
            Assert.AreEqual(context.SalesPoints.First(), context.Sales.First().SalesPoint);
            Assert.AreEqual(context.Buyers.First(), context.Sales.First().Buyer);
            Assert.AreEqual(300, context.Sales.First().TotalAmount);
            Assert.AreEqual(context.SalesData.First(), context.Sales.First().SalesData.First());
        }

        [TestMethod]
        public void TestMainController()
        {

        }

        private void FillDb()
        {
            var buyer = new Buyer("test buyer");
            var product = new Product("test product", 100.5f);
            var provided = new ProvidedProduct(product, 10);
            var salesPoint = new SalesPoint("test sales point", new List<ProvidedProduct> { provided });
            var saleData = new SaleData(product, 10, 200);
            date = new DateTime(2022, 6, 4);
            time = new DateTime(1, 1, 1, 12, 30, 40);
            var sale = new Sale(date, time, salesPoint, buyer, new List<SaleData> {saleData}, 300);

            context.Buyers.Add(buyer);
            context.Products.Add(product);
            context.ProvidedProducts.Add(provided);
            context.SalesPoints.Add(salesPoint);
            context.SalesData.Add(saleData);
            context.Sales.Add(sale);

            context.SaveChanges();
        }

        private MyDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            return new MyDbContext(options);
        }
    }
}