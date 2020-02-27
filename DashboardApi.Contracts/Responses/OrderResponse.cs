using Newtonsoft.Json;
using System;

namespace DashboardApi.Contracts.Responses
{
    public class OrderResponse
    {
        // The reason of setting Order values to -2 is that every property without an explicit Order value has a value of -1
        // by default. So you need to either give all child properties an Order value, or just set your base class' properties to -2.
        // We do this so the derived class CustomerOrderResponse's properties aren't #1
        [JsonProperty(Order = -2)]
        public int Id { get; set; }
        [JsonProperty(Order = -2)]
        public decimal Total { get; set; }
        [JsonProperty(Order = -2)]
        public DateTime Placed { get; set; }
        [JsonProperty(Order = -2)]
        public DateTime? Completed { get; set; }
    }
}
