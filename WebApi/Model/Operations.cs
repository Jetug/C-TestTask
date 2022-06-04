using EntityFramework.Data;
using EntityFramework.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Model
{
    public class Operations
    {
        private readonly MyDbContext dbContext;

        public Operations(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Sale(int? userId, int productId, int quantity)
        {
            var salePoints = dbContext.SalesPoints;

            if (salePoints == null) 
                return false;

            foreach (var salesPoint in salePoints)
            {
                var providedProducts = salesPoint.ProvidedProducts;

                if (providedProducts == null) continue;

                foreach (var providedProduct in providedProducts)
                {
                    if(providedProduct.ProductId == productId && providedProduct.ProductQuantity >= quantity)
                    {
                        providedProduct.ProductQuantity -= quantity;
                        Product product = dbContext.Products.Find(productId);

                        if (product == null) continue;

                        float productIdAmount = product.Price * quantity;
                        SaleData saleData = new(product, quantity, productIdAmount);
                        Sale sale = CreateSale(salesPoint, userId, new List<SaleData> { saleData }, productIdAmount);
                        dbContext.Sales.Add(sale);
                        dbContext.SaveChanges();

                        return true;
                    };
                }
            }
            return false;
        }

        private Sale CreateSale(SalesPoint salesPoint, int? bayerId, List<SaleData> salesData, float totalAmount)
        {
            Buyer buyer = null;

            if (bayerId != null)
            {
                buyer = dbContext.GetBuyer((int)bayerId);
            }

            return new Sale(System.DateTime.Now, System.DateTime.Today, salesPoint, buyer, salesData, totalAmount);
        }
    }
}