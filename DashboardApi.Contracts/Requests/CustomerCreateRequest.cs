using System.ComponentModel.DataAnnotations;

namespace DashboardApi.Contracts.Requests
{
    public class CustomerCreateRequest
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string State { get; set; }

    }
}
