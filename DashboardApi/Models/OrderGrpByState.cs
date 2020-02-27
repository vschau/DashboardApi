using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Models
{
    public class OrderGrpByState
    {
        public string State { get; set; }
        public decimal Total { get; set; }
    }
}
