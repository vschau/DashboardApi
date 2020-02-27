using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardApi.Contracts.Responses
{
    public class OrderGrpByStateResponse
    {
        public string State { get; set; }
        public decimal Total { get; set; }
    }
}
