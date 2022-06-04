using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Models
{
    public class Sale : IIdEntity
    {
        public Sale(){}

        public Sale(DateTime date, DateTime time, SalesPoint salesPoint, Buyer buyer, List<SaleData> salesData, float totalAmount)
        {
            Date = date;
            Time = time;
            SalesPoint = salesPoint;
            Buyer = buyer;
            SalesData = salesData;
            TotalAmount = totalAmount;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public int SalesPointId { get; set; }
        public SalesPoint SalesPoint { get; set; }

        public int? BayerId { get; set; }
        public Buyer Buyer { get; set; }

        public List<SaleData> SalesData { get; set; }
        public float TotalAmount { get; set; }
    }
}
