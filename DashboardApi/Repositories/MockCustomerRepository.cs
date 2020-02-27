using DashboardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Repositories
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private List<Customer> _customerList;
        public MockCustomerRepository()
        {
            _customerList = new List<Customer>()
            {
                new Customer() { Id = 1, Name = "Vanessa", Email = "vanessa@myemail.com", State = "CA" },
                new Customer() { Id = 1, Name = "John Doe", Email = "john@myemail.com", State = "NY" },
                new Customer() { Id = 1, Name = "Jane Doe", Email = "jane@myemail.com", State = "NV" }
            };
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Task.FromResult<List<Customer>>(_customerList);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await Task.Run(
                () => _customerList.Where(x => x.Id == id).SingleOrDefault()
            );
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            customer.Id = _customerList.Max(e => e.Id) + 1;
            _customerList.Add(customer);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customerChanges)
        {
            Customer customer = _customerList.FirstOrDefault(e => e.Id == customerChanges.Id);
            if (customer != null)
            {
                customer.Name = customerChanges.Name;
                customer.Email = customerChanges.Email;
                customer.State = customerChanges.State;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            Customer customer = _customerList.FirstOrDefault(e => e.Id == id);
            if (customer != null)
            {
                _customerList.Remove(customer);
            }

            return await Task.FromResult(true);
        }
    }
}
