using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class SaleData
    {
        public SaleData(){}

        public SaleData(int productId, int productQuantity, float productIdAmount)
        {
            ProductId = productId;
            ProductQuantity = productQuantity;
            ProductIdAmount = productIdAmount;
        }

        public int Id { get; set; }
        [ForeignKey(Product.tableName)]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductIdAmount { get; set; }
    }
}
