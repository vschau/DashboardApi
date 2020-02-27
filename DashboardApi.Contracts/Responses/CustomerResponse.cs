using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardApi.Contracts.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string State { get; set; }

        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
