using DashboardApi.Models;
using DashboardApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DashboardApi.Contracts.Requests;
using DashboardApi.Contracts.Responses;
using AutoMapper;
using System.Collections.Generic;

namespace DashboardApi.Controllers
{
    // TODO: add versioning
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();

            // Note that customer here is List<Customer>
            var customerResponse = _mapper.Map<List<CustomerResponse>>(customers);
            //var customerResponse = customers.Select(cust => new CustomerResponse
            //{
            //    Id = cust.Id,
            //    Name = cust.Name,
            //    Email = cust.Email,
            //    State = cust.State,
            //    Orders = cust.Orders.Select(t => new OrderResponse
            //    {
            //        Id = t.Id,
            //        Total = t.Total,
            //        Placed = t.Placed,
            //        Completed = t.Completed,
            //    })
            //});

            return Ok(customerResponse);
        }

        // GET /api/customers/id
        [HttpGet("{id}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            //var customerResponse = new CustomerResponse
            //{
            //    Id = customer.Id,
            //    Name = customer.Name,
            //    Email = customer.Email,
            //    State = customer.State,
            //    Orders = customer.Orders.Select(t => new OrderResponse
            //    {
            //        Id = t.Id,
            //        Total = t.Total,
            //        Placed = t.Placed,
            //        Completed = t.Completed,
            //    })
            //};

            return Ok(customerResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest model)
        {
            if (model == null)
                return BadRequest();

            var customer = new Customer {
                Name = model.Name,
                Email = model.Email,
                State = model.State
            };

            // This line will add id to our customer obj
            await _customerRepository.CreateCustomerAsync(customer);

            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            //var customerResponse = new CustomerResponse
            //{
            //    Id = customer.Id,
            //    Name = customer.Name,
            //    Email = customer.Email,
            //    State = customer.State,
            //    // New customer will have no orders
            //    Orders = Enumerable.Empty<OrderResponse>()
            //};

            // Note that the 1st param here is routename which is decorated in GetCustomerById HttpGet action. [HttpGet("{id}", Name= "NewName")]
            // It also returns the full URL path.
            //return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);

            //return full Url path.  This must match the action name
            //return CreatedAtAction("GetCustomerById", new { id = customer.Id }, customer);

            return Created(new Uri($"/api/customers/{customer.Id}", UriKind.Relative), customerResponse);
        }

        // POST: /api/customers/id
        // Put to overwrite, post to update
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerUpdateRequest model)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            // customer already has id because of the search line GetCustomerByIdAsync
            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.State = model.State;

            var updated = await _customerRepository.UpdateCustomerAsync(customer);

            if (!updated)
                return NotFound();

            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            //var customerResponse = new CustomerResponse
            //{
            //    Id = customer.Id,
            //    Name = customer.Name,
            //    Email = customer.Email,
            //    State = customer.State,
            //    Orders = customer.Orders.Select(t => new OrderResponse
            //    {
            //        Id = t.Id,
            //        Total = t.Total,
            //        Placed = t.Placed,
            //        Completed = t.Completed
            //    })
            //};
            return Ok(customerResponse); // Can return nocontent() here
        }

        // DELETE: /api/customers/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _customerRepository.DeleteCustomerAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
