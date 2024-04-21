using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Caching;
using RestaurantOrderAPI.Application.Models.Requests;
using RestaurantOrderAPI.Application.Models.Requests.Dishes;
using RestaurantOrderAPI.Application.Models.Responses.Dishes;
using RestaurantOrderAPI.Application.Validators;
using RestaurantOrderAPI.Domain.Entities;
using RestaurantOrderAPI.Domain.Extensions;
using RestaurantOrderAPI.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestaurantOrderAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishesController
            (IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
        [Route("/dishes")]
        public IActionResult CreateDish
            ([Required] Guid idOrder, CreateDishRequest request)
        {
            if (ModelState.IsValid && (!request.Type.Equals(DishType.NoType)))
            {
                var createDish = request.ToEntity(idOrder);
                createDish.IdOrder = idOrder;
                var result = _dishService.CreateDish
                (idOrder, createDish, d => d.Equals(createDish));
                if (result)
                {
                    var response = new CreateDishResponse
                        (new Application.Models.Dtos.DishDto(createDish));
                    return StatusCode(201, response);
                }
                return BadRequest("Dish not created");
            }
            return BadRequest("Request not valid");
        }
        [HttpGet]
        [Route("/dishes/{idDish}")]
        public async Task<IActionResult> GetDishAsync
            ([Required]Guid idOrder, [Required]Guid idDish)
        {
            var getDish = await _dishService.GetDishByIdAsync(idOrder, idDish);
            if (getDish != null)
            {
                var response = new GetDishResponse
                    (new Application.Models.Dtos.DishDto(getDish));
                return Ok(response);
            }
            return StatusCode
                (406, "No content found based on criteria");
        }
        [HttpPut]
        [Route("/dishes/{idDish}")]
        public async Task<IActionResult> UpdateDishAsync
            ([Required]Guid idOrder, [Required]Guid idDish, UpdateDishRequest request)
        {
            if (ModelState.IsValid && (!request.Type.Equals(DishType.NoType)))
            {
                var updateDish = await _dishService.GetDishByIdAsync(idOrder, idDish);
                if (updateDish != null)
                {
                    updateDish.Name = request.Name;
                    updateDish.Price = request.Price;
                    updateDish.Type = request.Type;
                    var result = await _dishService.UpdateDishAsync
                        (idOrder, updateDish, d => d.Equals(updateDish));
                    if (result)
                    {
                        var response = new UpdateDishResponse
                        (new Application.Models.Dtos.DishDto(updateDish));
                        return StatusCode(202, response);
                    }
                    return BadRequest("Dish not updated");
                }
                return StatusCode
                    (406, "No content found based on criteria");
            }
            return BadRequest("Request not valid");
        }
        [HttpDelete]
        [Route("/dishes/{idDish}")]
        public async Task<IActionResult> DeleteDishAsync
            ([Required]Guid idOrder, [Required]Guid idDish)
        {
            var deleteDish = await _dishService.GetDishByIdAsync(idOrder, idDish);
            if (deleteDish != null)
            {
                var result = await _dishService.DeleteDishAsync(idOrder, idDish);
                if (result)
                {
                    var response = new DeleteDishResponse
                        (new Application.Models.Dtos.DishDto(deleteDish));
                    return Ok(response);
                }
                return BadRequest("Dish not deleted");
            }
            return StatusCode
                (406, "No content found based on criteria");
        }
        /*
         * TODO : GET METHOD CANNOT HAVE JSON BODY
        [HttpPost]
        [Route("/dishes")]
        public async Task<IActionResult> GetDishesAsync
            ([Required]Guid idOrder, GetDishesRequest request)
        {
            if (ModelState.IsValid)
            {
                var dishes = await _dishService.GetAllDishAsync(idOrder, (dish =>
                {
                    return dish.IdOrder.Equals(idOrder) && (dish.Name.ToLower().Contains(name.ToLower()))
                    && ((price != 0) ? dish.Price.Equals(price) : true)
                    && ((type != DishType.NoType)
                        ? dish.Type.Equals(type) : true);
                }));
                if (dishes != null)
                {
                    var response = new GetDishesResponse(dishes);
                    return Ok(response);
                }
                //TODO : 410
                return BadRequest();
            } else
            {
                return BadRequest();
            }
        }
        */
        [HttpGet]
        [Route("/dishes")]
        public IActionResult GetDishes
            ([Required] Guid idOrder, int from, int num, string name = "",
            decimal price = 0, DishType type = DishType.NoType)
        {
            if (GetDishesValidation(name, price, type))
            {
                int queryCount = 0;
                var dishes = _dishService.GetAllDish(idOrder, from * num, from, (dish =>
                {
                    return dish.IdOrder.Equals(idOrder) 
                        && (dish.Name.ToLower().Contains(name.ToLower()))
                        && ((price != 0) ? dish.Price.Equals(price) : true)
                        && ((type != DishType.NoType) ? dish.Type.Equals(type) : true);
                }), out queryCount);
                if (dishes != null)
                {
                    var response = new GetDishesResponse();
                    var foundPages = (queryCount / (decimal)(from));
                    response.PagesNumber = (int)(Math.Ceiling(foundPages));
                    response.Dishes = dishes.Select(dish =>
                        new Application.Models.Dtos.DishDto(dish)).ToList();
                    return Ok(response);
                }
                return StatusCode
                    (406, "No content found based on criteria");
            }
            return BadRequest("Request not valid");
        }
        [HttpGet]
        [Route("/dishes/dishtypes")]
        public IActionResult GetDishTypes()
        {
            //TODO : OPTIMIZE THIS
            var result = new List<string>()
            {
                "List of available dish types",
                "s.m.inv.: enter only the associated number",
                "in the sections related to DishType",
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
            (string name = "", decimal price = 0, DishType type = DishType.NoType)
        {
            var nameValidation = new StringValidation();
            var typeValidation = new DishTypeValidation();
            return (nameValidation.Validate(name).IsValid) 
                && (typeValidation.Validate(type).IsValid) 
                && (price >= 0);
        }
    }
}
