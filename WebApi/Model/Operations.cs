using EntityFramework.Data;
using EntityFramework.Data.Tables;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Model
{
    public class Operations
    {
        private readonly MyDbContext myDbContext;

        public Operations(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public bool Sale(int? userId, int productId, int quantity)
        {
            var salePoints = myDbContext.SalesPoints;

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

                        var product = myDbContext.Products.Find(productId);
                        var productIdAmount = product.Price * quantity;
                        var saleData = new SaleData(productId, quantity, productIdAmount);
                        var sale = CreateSale(salesPoint.Id, userId, new List<SaleData> { saleData }, productIdAmount);

                        myDbContext.Sales.Add(sale);
                        myDbContext.SaveChanges();

                        if (userId != null)
                        {
                            var buyer = myDbContext.Buyers.Find(userId);
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
            return new Sale(System.DateTime.Now, System.DateTime.Today, salesPointId, bayerId, salesData, totalAmount);
        }
    }
}
