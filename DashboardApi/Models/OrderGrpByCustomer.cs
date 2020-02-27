using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Models
{
    public class OrderGrpByCustomer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}
