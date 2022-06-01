using EntityFramework.Data;
using EntityFramework.Data.Tables;
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

            foreach (var salesPoint in salePoints)
            {
                var providedProducts = salesPoint.ProvidedProducts;

                foreach (var providedProduct in providedProducts)
                {
                    if(providedProduct.ProductId == productId && providedProduct.ProductQuantity >= quantity)
                    {
                        providedProduct.ProductQuantity -= quantity;

                        if (providedProduct.ProductQuantity == 0)
                        {
                            providedProducts.Remove(providedProduct);
                        }

                        Product product = dbContext.Products.Find(productId);
                        float productIdAmount = product.Price * quantity;
                        SaleData saleData = new(product, quantity, productIdAmount);
                        Sale sale = CreateSale(salesPoint.Id, userId, new List<SaleData> { saleData }, productIdAmount);

                        dbContext.Sales.Add(sale);
                        dbContext.SaveChanges();

                        if (userId != null)
                        {
                            Buyer buyer = dbContext.Buyers.Find(userId);
                            buyer.SaleIds.Add(sale);
                        }

                        return true;
                    };
                }
            }
            return false;
        }

        private Sale CreateSale(int salesPointId, int? bayerId, List<SaleData> salesData, float totalAmount)
        {
            SalesPoint salesPoint = dbContext.GetSalesPoint(salesPointId);

            Buyer buyer = null;
            if (bayerId != null)
            {
                buyer = dbContext.GetBuyer((int)bayerId);
            }

            return new Sale(System.DateTime.Now, System.DateTime.Today, salesPoint, buyer, salesData, totalAmount);
        }
    }
}