using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Models
{
    public class SalesPoint : IIdEntity
    {
        public SalesPoint(){}

        public SalesPoint(string name, List<ProvidedProduct> providedProducts = null)
        {
            Name = name;
            ProvidedProducts = providedProducts;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProvidedProduct> ProvidedProducts { get; set; }
    }
}
