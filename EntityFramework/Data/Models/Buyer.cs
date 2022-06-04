using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Models
{
    public class Buyer : IIdEntity
    {
        public Buyer() { }

        public Buyer(string name, List<Sale> saleIds = null)
        {
            Name = name;
            SaleIds = saleIds;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Sale> SaleIds { get; set; }
    }
}