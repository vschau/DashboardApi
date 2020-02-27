using System.ComponentModel.DataAnnotations;

namespace DashboardApi.Contracts.Requests
{
    public class CustomerUpdateRequest : CustomerCreateRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
