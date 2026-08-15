using System;
using System.Collections.Generic;

namespace CoreErpService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Completed";

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}