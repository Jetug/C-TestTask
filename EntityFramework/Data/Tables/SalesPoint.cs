using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class SalesPoint
    {
        public const string tableName = "SalesPoint";

        public SalesPoint(){}

        public SalesPoint(string name, List<ProvidedProduct> providedProducts)
        {
            Name = name;
            ProvidedProducts = providedProducts;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProvidedProduct> ProvidedProducts { get; set; }
    }
}
