using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Cutomer { get; set; }
    }
}
