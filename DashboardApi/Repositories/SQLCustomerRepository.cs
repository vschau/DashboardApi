using DashboardApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Repositories
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private DashboardContext _context;

        public SQLCustomerRepository(DashboardContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.AsNoTracking().Include(x => x.Orders).ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking().Include(x => x.Orders).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);

            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
