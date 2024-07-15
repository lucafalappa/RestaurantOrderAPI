using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.Application.Abstractions.Services.Restaurant;
using RestaurantOrderAPI.Application.Models.Requests.Restaurant.Orders;
using RestaurantOrderAPI.Application.Models.Responses.Orders;
using RestaurantOrderAPI.Domain.Extensions;
using RestaurantOrderAPI.Entities.Restaurant;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.WebAPI.Controllers.Restaurant
{
    /// <summary>
    /// Controller responsible for order-related operations 
    /// in the web application
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The service for managing orders
        /// </summary>
        private readonly IOrderService _orderService;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="OrdersController"/> class
        /// </summary>
        /// <param name="orderService">The order service instance
        /// </param>
        public OrdersController
            (IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// Endpoint for creating a new order
        /// </summary>
        /// <param name="request">The create order request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// create order operation result
        /// </returns>
        [HttpPost]
        [Route("/orders")]
        public async Task<IActionResult> CreateOrderAsync
            ([FromBody][Required] CreateOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                var orderDto = request.ToDto();
                orderDto.IdUser = GetIdUser();
                orderDto.TotalPrice = _orderService.ApplyFullMealDiscount(orderDto);
                var result = await _orderService.CreateOrderAsync
                    (orderDto, order => order.IdOrder.Equals(orderDto.IdOrder));
                if (result != null)
                {
                    var response = new CreateOrderResponse(orderDto);
                    return StatusCode(201, response);
                }
                return StatusCode(400, "Order not created");
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for retrieving an order by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the order to retrieve
        /// </param>
        /// <returns>An IActionResult representing the 
        /// order retrieval operation result
        /// </returns>
        [HttpGet]
        [Route("/orders/{id}")]
        public async Task<IActionResult> GetOrderByIdAsync
            ([FromRoute][Required] Guid id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            if (result != null)
            {
                var response = new GetOrderResponse(result);
                return StatusCode(200, response);
            }
            return StatusCode(204);
        }
        /// <summary>
        /// Endpoint for updating an order's information
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the order to update
        /// </param>
        /// <param name="request">The update order request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// update order operation result
        /// </returns>
        [HttpPut]
        [Route("/orders/{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> UpdateOrderAsync
            ([FromRoute][Required] Guid id,
            [FromBody][Required] UpdateOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                var orderDto = await _orderService.GetOrderByIdAsync(id);
                if (orderDto != null)
                {
                    orderDto.DeliveryAddress = request.DeliveryAddress;
                    var result = await _orderService.UpdateOrderAsync
                        (orderDto, order => order.IdOrder.Equals(orderDto.IdOrder));
                    if (result != null)
                    {
                        var response = new UpdateOrderResponse(result);
                        return StatusCode(200, response);
                    }
                    return StatusCode(400, "Order not updated");
                }
                return StatusCode(406, "No content found based on criteria");
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for deleting an order by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the order to delete
        /// </param>
        /// <returns>An IActionResult representing the 
        /// delete order operation result
        /// </returns>
        [HttpDelete]
        [Route("/orders/{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> DeleteOrderAsync
            ([FromRoute][Required] Guid id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (result != null)
            {
                var response = new DeleteOrderResponse(result);
                return StatusCode(200, result);
            }
            return StatusCode(406, "No content found based on criteria");
        }
        /// <summary>
        /// Endpoint for retrieving a list of orders 
        /// with pagination and optional filters
        /// </summary>
        /// <param name="from">The starting index of the pagination
        /// </param>
        /// <param name="num">The number of items per page
        /// </param>
        /// <param name="startDate">Filter by minimum date
        /// </param>
        /// <param name="endDate">Filter by maximum date
        /// </param>
        /// <returns>An IActionResult representing the 
        /// order retrieval operation result
        /// </returns>
        [HttpGet]
        [Route("/orders")]
        public IActionResult GetOrders
            ([FromQuery][Required] int from,
            [FromQuery][Required] int num,
            [FromQuery] string startDate = "",
            [FromQuery] string endDate = "")
        {
            DateTime? parsedStartDate = startDate.ToNullableDateTime();
            DateTime? parsedEndDate = endDate.ToNullableDateTime();
            parsedStartDate = (parsedStartDate != null) ? parsedStartDate : DateTime.MinValue;
            parsedEndDate = (parsedEndDate != null) ? parsedEndDate : DateTime.MaxValue;
            var userRole = this.User.FindFirst
                (claim => claim.Type.Equals("Role"))!.Value;
            int queryCount = 0;
            Expression<Func<Order, bool>> predicate;
            if (userRole.Equals("Customer"))
            {
                predicate = (order =>
                    ((order.OrderDate >= parsedStartDate)
                        && (order.OrderDate <= parsedEndDate))
                    && order.IdUser.Equals(GetIdUser())
                );
            }
            else
            {
                predicate = (order =>
                    ((order.OrderDate >= parsedStartDate)
                    && (order.OrderDate <= parsedEndDate))
                );
            }
            var orderDtos = _orderService.GetOrdersWithPagination
                    (from * num, from, predicate, out queryCount);
            if (orderDtos.Count != 0)
            {
                var response = new GetOrdersResponse(orderDtos);
                var foundPages = (queryCount / (decimal)(from));
                response.PagesNumber = (int)(Math.Ceiling(foundPages));
                return Ok(response);
            }
            return StatusCode
                (406, "No content found based on criteria");
        }
        private Guid GetIdUser()
        {
            Guid result;
            Guid.TryParse(this.User.FindFirst("IdUser")?.Value, out result);
            return result;
        }
        private bool IsDateBetween
            (DateTime target, DateTime? start, DateTime? end)
        {
            start = (start != null) ? start : DateTime.MinValue;
            end = (end != null) ? end : DateTime.MaxValue;
            return target >= start && target <= end;
        }
    }
}
