using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DashboardApi.Models;
using DashboardApi.Repositories;
using DashboardApi.Contracts.Requests;
using DashboardApi.Contracts.Requests.Queries;
using DashboardApi.Contracts.Responses;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

namespace DashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery]PaginationQuery paginationQuery)
        {
            paginationQuery.Filter = paginationQuery?.Filter?.ToLower(); 

            if (!paginationQuery.SortDirection.Equals("asc") && !paginationQuery.SortDirection.Equals("desc"))
            {
                paginationQuery.SortDirection = "asc";
            }

            if (paginationQuery.PageSize < 1 || paginationQuery.PageIndex < 1)
            {
                paginationQuery = new PaginationQuery(paginationQuery.Filter, paginationQuery.SortColumn, paginationQuery.SortDirection, 1, 100); // Set to default 1:100
            }

            int totalCount = await _orderRepository.GetTotalCountAsync();
            var orders = await _orderRepository.GetOrdersAsync(paginationQuery);

            return Ok(new PagedResponse<CustomerOrderResponse>
            {
                Data = _mapper.Map<List<CustomerOrderResponse>>(orders),
                TotalCount = totalCount,
                PageSize = paginationQuery.PageSize,
                pageIndex = paginationQuery.PageIndex,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)paginationQuery.PageSize)
            });
        }

        // GET: api/orders/bystate
        [HttpGet("ByState")]
        public async Task<IActionResult> GetOrdersGrpByState()
        {
            var orders = await _orderRepository.GetOrdersGrpByStateAsync();

            var orderResponse = _mapper.Map<List<OrderGrpByStateResponse>>(orders);

            return Ok(orderResponse);
        }

        // GET: api/orders/bycustomer
        // GET: api/orders/bycustomer/MaxNumber
        [HttpGet("ByCustomer/{n?}")]
        public async Task<IActionResult> GetOrdersGrpByState(int? n)
        {
            var orders = await _orderRepository.GetOrdersGrpByCustomerAsync(n ?? 100);

            var orderResponse = _mapper.Map<List<OrderGrpByCustomerResponse>>(orders);
            return Ok(orderResponse);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return Ok(orderResponse);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderUpdateRequest model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var order = await _orderRepository.GetOrderByIdAsync(id);
            
            if (order == null)
                return NotFound();

            order.Total = model.Total;
            order.Placed = model.Placed;
            order.Completed = model.Completed;

            var updated = await _orderRepository.UpdateOrderAsync(order);

            if (!updated)
                return NoContent();

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return Ok(orderResponse);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderCreateRequest model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(model);

            await _orderRepository.CreateOrderAsync(order);

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return Created(new Uri($"/api/orders/{order.Id}", UriKind.Relative), orderResponse);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var deleted = await _orderRepository.DeleteOrderAsync(id);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
