using DashboardApi.Contracts.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace DashboardApi.Contracts.Requests
{
    public class OrderCreateRequest
    {
        [Required]
        public decimal Total { get; set; }

        [RequiredDateTimeValidator]
        public DateTime Placed { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? Completed { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
    }
}
