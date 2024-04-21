using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Caching;
using RestaurantOrderAPI.Application.Models.Requests.Orders;
using RestaurantOrderAPI.Application.Models.Responses.Orders;
using RestaurantOrderAPI.Application.Validators;
using RestaurantOrderAPI.Domain.Entities;
using RestaurantOrderAPI.Domain.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Xml.Linq;

namespace RestaurantOrderAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController
            (IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        [Route("/orders/new")]
        public IActionResult GetNewIdOrder()
        {
            return Ok(Guid.NewGuid().ToString());
        }
        [HttpPost]
        [Route("/orders/new/{idOrder}")]
        public async Task<IActionResult> CreateNewOrderAsync
            ([Required]Guid idOrder, CreateOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                var createOrder = request.ToEntity(idOrder);
                var result = await _orderService.CreateOrderAsync
                    (GetIdUser(), createOrder, o => o.Equals(createOrder));
                if (result)
                {
                    var response = new CreateOrderResponse
                        (new Application.Models.Dtos.OrderDto(createOrder), 
                        ApplyFullMealDiscount(createOrder));
                    return Ok(response);
                }
                return BadRequest("Order not created");
            }
            return BadRequest("Request not valid");
        }
        [HttpGet]
        [Route("/orders/{idOrder}")]
        public async Task<IActionResult> GetOrderAsync
            ([Required]Guid idOrder)
        {
            var getOrder = await _orderService.GetOrderByIdAsync(GetIdUser(), idOrder);
            if (getOrder != null)
            {
                var response = new GetOrderResponse
                    (new Application.Models.Dtos.OrderDto(getOrder));
                return Ok(response);
            }
            return StatusCode
                (406, "No content found based on criteria");
        }
        [HttpPut]
        [Route("/orders/{idOrder}")]
        public async Task<IActionResult> UpdateOrderAsync
            ([Required]Guid idOrder, UpdateOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                var updateOrder = await _orderService.GetOrderByIdAsync(GetIdUser(), idOrder);
                if (updateOrder != null)
                {
                    updateOrder.DeliveryAddress = request.DeliveryAddress;
                    var result = await _orderService.UpdateOrderAsync
                        (GetIdUser(), updateOrder, o => o.Equals(updateOrder));
                    if (result)
                    {
                        var response = new UpdateOrderResponse
                        (new Application.Models.Dtos.OrderDto(updateOrder));
                        return Ok(response);
                    }
                    return BadRequest("Order not updated");
                }
                return StatusCode
                    (406, "No content found based on criteria");
            }
            return BadRequest("Request not valid");
        }
        [HttpDelete]
        [Route("/orders/{idOrder}")]
        public async Task<IActionResult> DeleteOrderAsync
            ([Required]Guid idOrder)
        {
            var deleteOrder = await _orderService.GetOrderByIdAsync(GetIdUser(), idOrder);
            if (deleteOrder != null)
            {
                var result = await _orderService.DeleteOrderAsync(GetIdUser(), idOrder);
                if (result)
                {
                    var response = new DeleteOrderResponse
                        (new Application.Models.Dtos.OrderDto(deleteOrder));
                    return Ok(response);
                }
                return BadRequest("Order not deleted");
            }
            return StatusCode
                (406, "No content found based on criteria");
        }
        /*
         * TODO : GET METHOD CANNOT HAVE JSON BODY
        [HttpGet]
        [Route("/orders")]
        public async Task<IActionResult> GetOrdersAsync
            ([Required]Guid idUser, GetOrdersRequest request)
        {
            if (ModelState.IsValid)
            {
                var orders = await _orderService.GetAllOrderAsync(idUser, (order =>
                {
                    return IsDateBetween
                    (order.OrderDate, request.StartDate, request.EndDate);
                }));
                if (orders != null)
                {
                    var response = new GetOrdersResponse(orders);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            } else
            {
                return BadRequest();
            }
        }
        */
        [HttpGet]
        [Route("/orders")]
        public IActionResult GetOrders
            (DateTime startDate, DateTime endDate, int from, int num)
        {
            if (GetOrdersValidation(startDate, endDate))
            {
                int queryCount = 0;
                List<Order> orders = null!;
                if (this.User.FindFirst(claim =>
                    (claim.Type.Equals("Role")) 
                    && (claim.Value.Equals("Customer"))) != null)
                {
                    orders = _orderService.GetAllOrder(GetIdUser(), 
                        from * num, from, (order =>
                    {
                        return IsDateBetween(order.OrderDate, startDate, endDate) 
                            && order.IdUser.Equals(GetIdUser());
                    }), out queryCount);
                }
                if (this.User.FindFirst(claim =>
                    (claim.Type.Equals("Role")) 
                    && (claim.Value.Equals("Administrator"))) != null)
                {
                    orders = _orderService.GetAllOrder(from * num, from, (order =>
                    {
                        return IsDateBetween
                        (order.OrderDate, startDate, endDate);
                    }), out queryCount);
                }
                if (orders != null)
                {
                    var response = new GetOrdersResponse();
                    var foundPages = (queryCount / (decimal)(from));
                    response.PagesNumber = (int)(Math.Ceiling(foundPages));
                    response.Orders = orders.Select(order =>
                    new Application.Models.Dtos.OrderDto(order)).ToList();
                    return Ok(response);
                }
                return StatusCode
                    (406, "No content found based on criteria");
            }
            return BadRequest("Request not valid");
        }
        private Guid GetIdUser()
        {
            Guid result;
            Guid.TryParse(this.User.FindFirst("IdUser")?.Value, out result);
            return result;
        }
        private bool IsDateBetween
            (DateTime target, DateTime start, DateTime end)
        {
            return target >= start && target <= end;
        }
        private bool GetOrdersValidation
            (DateTime startDate, DateTime endDate)
        {
            var dateTimeValidation = new DateTimeValidation();
            return (dateTimeValidation.Validate(startDate).IsValid) 
                && (dateTimeValidation.Validate(endDate).IsValid);
        }
        private decimal ApplyFullMealDiscount(Order order)
        {
            //TODO : DISCOUNT IMPLEMENTED, BUT NOT VERIFIED
            var defaultTotalPrice = order.OrderedDishes.Sum(dish => dish.Price);
            var groups = order.OrderedDishes.GroupBy(dish => dish.Type);
            if (groups.Count() != 4)
            {
                return defaultTotalPrice;
            }
            var sublists = groups.Select(groups => groups.ToList()).ToList();
            sublists.ForEach(sublist => sublist.OrderByDescending(dish => dish.Price));
            decimal newTotalPrice = 0m; 
            foreach (var sublist in sublists)
            {
                newTotalPrice += sublist.First().Price * 0.9m;
                sublist.RemoveAt(0);
            }
            foreach (var sublist in sublists)
            {
                newTotalPrice += sublist.Sum(dish => dish.Price);
            }
            return newTotalPrice;
        }
    }
}
