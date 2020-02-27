using DashboardApi.Contracts.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace DashboardApi.Contracts.Requests
{
    public class OrderUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        public decimal Total { get; set; }

        // Need this but not total because placed is datetime and min is 1/1/0001.  It's a struct
        [RequiredDateTimeValidator]
        public DateTime Placed { get; set; }

        public DateTime? Completed { get; set; }

        // No CustomerId because we don't allow them to switch order from 1 customer to another.
    }
}
