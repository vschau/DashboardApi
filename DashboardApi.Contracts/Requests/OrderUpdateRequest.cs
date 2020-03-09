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

        [RequiredDateTimeValidator]
        public DateTime Placed { get; set; }

        public DateTime? Completed { get; set; }
    }
}
