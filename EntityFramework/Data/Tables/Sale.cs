using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int SalesPointId { get; set; }
        public int? BayerId { get; set; }
        public List<SaleData> SalesData { get; set; }
        public float TotalAmount { get; set; }
    }
}
