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
    // toDO: don't let it return "customer": null.  Use a response obj
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
        // if there's no query string for pagination, then paginationQuery is null (from IOrderRepository) but due to the constructor, it'll become 1:100
        // Thus, it's always 1:100 and we can't skip the totalCount await.  But it's good that we return 100 records only
        // Let's not focus on optimization for now but functionality
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
                //Data = orders.Select(order => new CustomerOrderResponse
                //{
                //    Id = order.Id,
                //    Total = order.Total,
                //    Placed = order.Placed,
                //    Completed = order.Completed,
                //    CustomerId = order.CustomerId,
                //    CustomerName = order.Cutomer.Name
                //}),
                Data = _mapper.Map<List<CustomerOrderResponse>>(orders),
                TotalCount = totalCount,
                PageSize = paginationQuery.PageSize,
                pageIndex = paginationQuery.PageIndex,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)paginationQuery.PageSize)
            });
        }

        //public async Task<IActionResult> GetOrders([FromQuery]PaginationQuery paginationQuery)
        //{
        //    var orders = await _orderRepository.GetOrdersAsync(paginationQuery);

        //    // Count of every single orders, not depending on paginationQuery
        //    var totalCount = await _orderRepository.GetTotalCountAsync();
        //    var totalPages = Math.Ceiling(totalCount / (decimal)paginationQuery.PageSize);

        //    var page = new PaginatedResponse<Order>
        //    {
        //        Data = orders,
        //        TotalCount = totalCount
        //    };
        //    var response = new
        //    {
        //        Page = page,
        //        TotalPages = totalPages
        //    };

        //    return Ok(response);
        //}

        // GET: api/orders/bystate
        // Note that the response shape is different so we'll make a new endpoint
        [HttpGet("ByState")]
        public async Task<IActionResult> GetOrdersGrpByState()
        {
            var orders = await _orderRepository.GetOrdersGrpByStateAsync();

            var orderResponse = _mapper.Map<List<OrderGrpByStateResponse>>(orders);
            //var orderResponse = orders.Select(o => new OrderGrpByStateResponse
            //{
            //    State = o.State,
            //    Total = o.Total
            //});

            return Ok(orderResponse);
        }

        // GET: api/orders/bycustomer
        // GET: api/orders/bycustomer/MaxNumber
        [HttpGet("ByCustomer/{n?}")]
        public async Task<IActionResult> GetOrdersGrpByState(int? n)
        {
            var orders = await _orderRepository.GetOrdersGrpByCustomerAsync(n ?? 100);

            var orderResponse = _mapper.Map<List<OrderGrpByCustomerResponse>>(orders);
            //var orderResponse = orders.Select(o => new OrderGrpByCustomerResponse
            //{
            //    CustomerId = o.CustomerId,
            //    Name = o.Name,
            //    Total = o.Total
            //});

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
            //var orderResponse = new OrderResponse
            //{
            //    Id = order.Id,
            //    Total = order.Total,
            //    Placed = order.Placed,
            //    Completed = order.Completed,
            //};

            return Ok(orderResponse);
        }

        // PUT: api/Orders/5
        // Note: we do it differently here from CustomersController and have id field in OrderUpdateViewModel
        // Note: we have to trust that the model coming in will have full properties. PUT is for writing/replacing the entire resource.
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

            // order already has id because of the search line GetOrderByIdAsync
            order.Total = model.Total;
            order.Placed = model.Placed;
            order.Completed = model.Completed;

            var updated = await _orderRepository.UpdateOrderAsync(order);

            if (!updated)
                return NoContent();

            var orderResponse = _mapper.Map<OrderResponse>(order);
            //var orderResponse = new OrderResponse
            //{
            //    Id = order.Id,
            //    Total = order.Id,
            //    Placed = order.Placed,
            //    Completed = order.Completed
            //};

            return Ok(orderResponse);
        }

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderCreateRequest model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(model);
            //var order = new Order
            //{
            //    Total = model.Total,
            //    Placed = model.Placed,
            //    Completed = model.Completed,
            //    CustomerId = model.CustomerId
            //};

            // This adds Id to order obj
            await _orderRepository.CreateOrderAsync(order);

            var orderResponse = _mapper.Map<OrderResponse>(order);
            //var orderResponse = new OrderResponse
            //{
            //    Id = order.Id,
            //    Total = order.Id,
            //    Placed = order.Placed,
            //    Completed = order.Completed
            //};

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
