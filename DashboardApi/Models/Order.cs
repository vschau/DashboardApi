using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Models
{
    // A customer has multiple orders
    // Each order references to 1 customer
    // Customer (1) -> Order (Many)
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }


        // If we don't add this column, table still has it but we won't be able to access it in code
        public int CustomerId { get; set; }
        public virtual Customer Cutomer { get; set; }
    }
}
