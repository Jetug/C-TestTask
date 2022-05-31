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
        public int Id { get; set; }
        [ForeignKey(Product.tableName)]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
