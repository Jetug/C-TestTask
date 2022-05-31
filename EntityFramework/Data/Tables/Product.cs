using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class Product
    {
        public const string tableName = "Product"; 

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
