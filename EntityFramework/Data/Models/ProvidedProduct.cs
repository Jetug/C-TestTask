using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class ProvidedProduct
    {
        public ProvidedProduct(){}

        public ProvidedProduct(Product product, int productQuantity)
        {
            Product = product;
            ProductQuantity = productQuantity;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public List<SalesPoint> SalesPoints { get; set; }
    }
}
