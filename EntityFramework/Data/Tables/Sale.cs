using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Tables
{
    public class Sale
    {
        public Sale(){}

        public Sale(DateTime date, DateTime time, int salesPointId, int? bayerId, List<SaleData> salesData, float totalAmount)
        {
            Date = date;
            Time = time;
            SalesPointId = salesPointId;
            BayerId = bayerId;
            SalesData = salesData;
            TotalAmount = totalAmount;
            string st = typeof(SalesPoint).Name;
        }
        

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey(SalesPoint.tableName)]
        public int SalesPointId { get; set; }
        [ForeignKey(Buyer.tableName)]
        public int? BayerId { get; set; }
        public List<SaleData> SalesData { get; set; }
        public float TotalAmount { get; set; }
    }
}
