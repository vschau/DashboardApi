using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardApi.Contracts.Responses
{
    public class CustomerOrderResponse: OrderResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
