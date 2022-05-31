using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class Buyer
    {
        public const string tableName = "Buyer";

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Sale> SaleIds { get; set; }
    }
}