using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class Product
    {
        public Product(){}

        public Product(string name, float price)
        {
            Name = name;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public ProvidedProduct ProvidedProduct { get; set; }
    }
}
