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

            await _customerRepository.CreateCustomerAsync(customer);

            var customerResponse = _mapper.Map<CustomerResponse>(customer);

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

            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.State = model.State;

            var updated = await _customerRepository.UpdateCustomerAsync(customer);

            if (!updated)
                return NotFound();

            var customerResponse = _mapper.Map<CustomerResponse>(customer);
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
