using Newtonsoft.Json;
using System;

namespace DashboardApi.Contracts.Responses
{
    public class OrderResponse
    {
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
