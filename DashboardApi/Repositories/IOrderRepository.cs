using DashboardApi.Models;
using DashboardApi.Contracts.Requests.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardApi.Repositories
{
    public interface IOrderRepository
    {
        Task<int> GetTotalCountAsync();
        Task<List<OrderGrpByState>> GetOrdersGrpByStateAsync();
        Task<List<OrderGrpByCustomer>> GetOrdersGrpByCustomerAsync(int n);
        Task<List<Order>> GetOrdersAsync(PaginationQuery paginationQuery = null);
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> CreateOrderAsync(Order Order);
        Task<bool> UpdateOrderAsync(Order Order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
