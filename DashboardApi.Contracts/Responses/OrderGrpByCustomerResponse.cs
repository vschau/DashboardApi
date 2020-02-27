using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardApi.Contracts.Responses
{
    public class OrderGrpByCustomerResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}
