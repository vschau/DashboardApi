using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DashboardApi.Models;
using DashboardApi.Contracts.Requests.Queries;
using System.Linq.Expressions;

namespace DashboardApi.Repositories
{
    public class SQLOrderRepository : IOrderRepository
    {
        private DashboardContext _context;

        public SQLOrderRepository(DashboardContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<List<Order>> GetOrdersAsync(PaginationQuery paginationQuery = null)
        {
            IQueryable<Order> queryable = _context.Orders.AsNoTracking().Include(x => x.Cutomer).AsQueryable();

            if (paginationQuery == null)
            {
                return await queryable.ToListAsync().ConfigureAwait(false);
            }

            if (!string.IsNullOrEmpty(paginationQuery.Filter))
            {
                queryable = queryable.Where(x => x.Cutomer.Name.Contains(paginationQuery.Filter));
            }

            if (paginationQuery.SortDirection == "asc")
            {
                queryable = queryable.OrderBy(OrderByFunc(paginationQuery.SortColumn));
            }
            else
            {
                queryable = queryable.OrderByDescending(OrderByFunc(paginationQuery.SortColumn));
            }

            var skip = (paginationQuery.PageIndex - 1) * paginationQuery.PageSize;

            return await queryable.Skip(skip).Take(paginationQuery.PageSize).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<OrderGrpByState>> GetOrdersGrpByStateAsync()
        {
            var orders = await (from c in _context.Customers
                                join o in _context.Orders on c.Id equals o.CustomerId
                                select new { c.State, o.Total } into g1
                                group g1 by g1.State into g2
                                select new OrderGrpByState {
                                    State = g2.Key,
                                    Total = g2.Sum(p => p.Total)
                                }).OrderByDescending(x => x.Total).ToListAsync().ConfigureAwait(false);

            return orders;
        }

        public async Task<List<OrderGrpByCustomer>> GetOrdersGrpByCustomerAsync(int n)
        {
            var orders = await (from c in _context.Customers
                          join o in _context.Orders on c.Id equals o.CustomerId
                          select new { c.Id, c.Name, o.Total } into g1
                          group g1 by new { g1.Id, g1.Name } into g2
                          select new OrderGrpByCustomer
                          {
                              CustomerId = g2.Key.Id,
                              Name = g2.Key.Name,
                              Total = g2.Sum(p => p.Total)
                          }).OrderByDescending(x => x.Total).Take(n).ToListAsync().ConfigureAwait(false);

            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(x => x.Cutomer).SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<bool> CreateOrderAsync(Order Order)
        {
            await _context.Orders.AddAsync(Order);

            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateOrderAsync(Order Order)
        {
            _context.Orders.Update(Order);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var Order = await GetOrderByIdAsync(id);
            if (Order == null)
                return false;

            _context.Orders.Remove(Order);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        /// <summary>
        /// Helper function
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        private Expression<Func<Order, Object>> OrderByFunc(string sortColumn)
        {
            var sortColumnLower = sortColumn.ToLower();
            Expression<Func<Order, Object>> orderByFunc = sortColumnLower switch
            {
                "id" => item => item.Id,
                "total" => item => item.Total,
                "placed" => item => item.Placed,
                "completed" => item => item.Completed,
                "customerid" => item => item.Cutomer.Id,
                "customername" => item => item.Cutomer.Name,
                "name" => item => item.Cutomer.Name,
                _ => item => item.Id.ToString()
            };
            return orderByFunc;
        }
    }
}
