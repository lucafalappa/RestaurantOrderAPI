using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.Application.Abstractions.Services.Restaurant;
using RestaurantOrderAPI.Application.Models.Requests.Restaurant.Dishes;
using RestaurantOrderAPI.Application.Models.Responses.Dishes;
using RestaurantOrderAPI.Application.Validators;
using RestaurantOrderAPI.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderAPI.WebAPI.Controllers.Restaurant
{
    /// <summary>
    /// Controller responsible for dish-related operations 
    /// in the web application
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DishesController : ControllerBase
    {
        /// <summary>
        /// The service for managing dishes
        /// </summary>
        private readonly IDishService _dishService;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DishesController"/> class
        /// </summary>
        /// <param name="dishService">The dish service instance
        /// </param>
        public DishesController
            (IDishService dishService)
        {
            _dishService = dishService;
        }
        /// <summary>
        /// Endpoint for creating a new dish, after creation of
        /// an order (required operation)
        /// </summary>
        /// <param name="request">The create dish request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// create dish operation result
        /// </returns>
        [HttpPost]
        [Route("/dishes")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> CreateDishAsync
            ([FromQuery][Required] Guid idOrder,
            [FromBody][Required] CreateDishRequest request)
        {
            if (ModelState.IsValid)
            {
                var dishDto = request.ToDto();
                dishDto.IdOrder = idOrder;
                var result = await _dishService.CreateDishAsync
                    (dishDto, dish => dish.IdDish.Equals(dishDto.IdDish));
                if (result != null)
                {
                    var response = new CreateDishResponse(result);
                    return StatusCode(201, response);
                }
                return StatusCode(400, "Dish not created");
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for retrieving a dish by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the dish to retrieve
        /// </param>
        /// <returns>An IActionResult representing the 
        /// dish retrieval operation result
        /// </returns>
        [HttpGet]
        [Route("/dishes/{id}")]
        public async Task<IActionResult> GetDishByIdAsync
            ([FromRoute][Required] Guid id)
        {
            var result = await _dishService.GetDishByIdAsync(id);
            if (result != null)
            {
                var response = new GetDishResponse(result);
                return StatusCode(200, response);
            }
            return StatusCode(204);
        }
        /// <summary>
        /// Endpoint for updating a dish's information
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the dish to update
        /// </param>
        /// <param name="request">The update dish request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// update dish operation result
        /// </returns>
        [HttpPut]
        [Route("/dishes/{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> UpdateDishAsync
            ([FromRoute][Required] Guid id,
            [FromBody][Required] UpdateDishRequest request)
        {
            if (ModelState.IsValid)
            {
                var dishDto = await _dishService.GetDishByIdAsync(id);
                if (dishDto != null)
                {
                    dishDto.Name = request.Name;
                    dishDto.Price = request.Price;
                    dishDto.Type = request.Type;
                    var result = await _dishService.UpdateDishAsync
                        (dishDto, dish => dish.IdDish.Equals(dishDto.IdDish));
                    if (result != null)
                    {
                        var response = new UpdateDishResponse(result);
                        return StatusCode(200, response);
                    }
                    return StatusCode(400, "Dish not updated");
                }
                return StatusCode(406, "No content found based on criteria");
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for deleting a dish by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the dish to delete
        /// </param>
        /// <returns>An IActionResult representing the 
        /// delete dish operation result
        /// </returns>
        [HttpDelete]
        [Route("/dishes/{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> DeleteDishAsync
            ([FromRoute][Required] Guid id)
        {
            var result = await _dishService.DeleteDishAsync(id);
            if (result != null)
            {
                var response = new DeleteDishResponse(result);
                return StatusCode(200, result);
            }
            return StatusCode(406, "No content found based on criteria");
        }
        /// <summary>
        /// Endpoint for retrieving a list of dishes 
        /// with pagination and optional filters
        /// </summary>
        /// <param name="idOrder">The unique identifier
        /// of the associated order
        /// </param>
        /// <param name="from">The starting index of the pagination
        /// </param>
        /// <param name="num">The number of items per page
        /// </param>
        /// <param name="name">Filter by dish name
        /// </param>
        /// <param name="price">Filter by dish price
        /// </param>
        /// <param name="type">Filter by dish type
        /// </param>
        /// <returns>An IActionResult representing the 
        /// dish retrieval operation result
        /// </returns>
        [HttpGet]
        [Route("/dishes")]
        public IActionResult GetDishes
            ([FromQuery][Required] Guid idOrder,
            [FromQuery][Required] int from,
            [FromQuery][Required] int num,
            [FromQuery] string name = "",
            [FromQuery] decimal price = 0,
            [FromQuery] DishType type = DishType.NoType)
        {
            if (this.GetDishesValidation(name, price))
            {
                int queryCount = 0;
                var dishes = _dishService.GetDishesWithPagination
                    (from * num, from, (dish =>
                    dish.IdOrder.Equals(idOrder)
                        && (dish.Name.ToLower().Contains(name.ToLower()))
                        && ((price != 0)
                            ? dish.Price.Equals(price)
                            : true)
                        && ((type != DishType.NoType)
                            ? dish.Type.Equals(type)
                            : true)
                    ), out queryCount);
                if (dishes.Count != 0)
                {
                    var response = new GetDishesResponse(dishes);
                    var foundPages = (queryCount / (decimal)(from));
                    response.PagesNumber = (int)(Math.Ceiling(foundPages));
                    return StatusCode(200, response);
                }
                return StatusCode(204);
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for retrieving a list of available dish types 
        /// with associated numeric values
        /// </summary>
        /// <returns>An IActionResult containing a list of strings representing available dish types.</returns>
        [HttpGet]
        [Route("/dishes/dishtypes")]
        public IActionResult GetDishTypes()
        {
            var result = new List<string>()
            {
                "List of available dish types",
                "s.m.inv.: enter only the associated number",
                "in the sections related num DishType",
                nameof(DishType.FirstCourse) + " = 1",
                nameof(DishType.SecondCourse) + " = 2",
                nameof(DishType.SideDish) + " = 3",
                nameof(DishType.Dessert) + " = 4",
                "s.m.inv.: DishType = 0 stays for NoType,",
                "and it's default value"
            };
            return Ok(result);
        }
        private bool GetDishesValidation
            (string name = "", decimal price = 0)
        {
            var nameValidation = new StringValidation();
            return (nameValidation.Validate(name).IsValid)
                && (price >= 0);
        }
    }
}
