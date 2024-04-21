using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Models.Responses.Dishes;
using RestaurantOrderAPI.Application.Models.Responses.Users;
using RestaurantOrderAPI.Application.Services;
using RestaurantOrderAPI.Domain.Entities;
using RestaurantOrderAPI.Domain.Extensions;
using RestaurantOrderAPI.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestaurantOrderAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UsersController
            (IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        
        [HttpPost]
        [Route("/users/login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var users = await _userService.GetAllUserAsync(user => 
                    user.Email.Equals(request.Email));
                var user = users.FirstOrDefault();
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                    {
                        var response = new LoginResponse
                            (_tokenService.CreateToken(user));
                        return Ok(response);
                    }
                    return BadRequest("Invalid password");
                }
                return BadRequest("Invalid email or password");
            }
            return BadRequest("Request not valid");
        }
        
        [HttpPost]
        [Route("/users/register")]
        public async Task<IActionResult> RegisterAsync
            (RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var userRoleToEnum = request.UserRole.ToEnum(UserRole.NoRole);
                if (!await _userService.NotUniqueAdministrator
                    (userRoleToEnum))
                {
                    var idUser = Guid.NewGuid();
                    var token = _tokenService.CreateToken(idUser, request);
                    var user = new User
                    {
                        IdUser = idUser,
                        Email = request.Email,
                        Name = request.Name,
                        Surname = request.Surname,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        Role = userRoleToEnum
                    };
                    var result = await _userService.CreateUserAsync
                        (user, u => u.Equals(user));
                    if (result)
                    {
                        var response = new RegisterResponse(token);
                        return Ok(response);
                    }
                    return BadRequest("User not created");
                }
                return BadRequest("Administrator already existing");
            }
            return BadRequest("Invalid request");
        }
        [HttpGet]
        [Route("/users/{idUser}")]
        public async Task<IActionResult> GetUserAsync
            ([Required]Guid idUser)
        {
            //TODO : IMPLEMENT ROLES
            if (this.User.FindFirst(claim => 
                (claim.Type.Equals("Role")) 
                && (claim.Value.Equals("Administrator"))) != null)
            {
                var getUser = await _userService.GetUserByIdAsync(idUser);
                if (getUser != null)
                {
                    var response = new GetUserResponse
                        (new Application.Models.Dtos.UserDto(getUser));
                    return Ok(response);
                }
                return StatusCode
                    (406, "No content found based on criteria");
            }
            return StatusCode(403, "Not allowed to perform this operation");
        }
        [HttpPut]
        [Route("/users/{idUser}")]
        public async Task<IActionResult> UpdateUserAsync
            ([Required]Guid idUser, UpdateUserRequest request)
        {
            var userRoleToEnum = request.UserRole.ToEnum(UserRole.NoRole);
            if (ModelState.IsValid && !(userRoleToEnum.Equals(UserRole.NoRole)))
            {
                if (!await _userService.NotUniqueAdministrator
                    (userRoleToEnum))
                {
                    var updateUser = await _userService.GetUserByIdAsync(idUser);
                    if (updateUser != null)
                    {
                        updateUser.Email = request.Email;
                        updateUser.Name = request.Name;
                        updateUser.Surname = request.Surname;
                        updateUser.Password = request.Password;
                        updateUser.Role = request.UserRole.ToEnum(UserRole.NoRole);
                        await _userService.UpdateUserAsync(updateUser, u => u.Equals(updateUser));
                        var response = new UpdateUserResponse
                            (new Application.Models.Dtos.UserDto(updateUser));
                        return Ok(response);
                    }
                    return StatusCode
                        (406, "No content found based on criteria");
                }
                return BadRequest("Administrator already existing");
            }
            return BadRequest("Request not valid");
        }
        [HttpDelete]
        [Route("/users/{idUser}")]
        public async Task<IActionResult> DeleteUserAsync
            ([Required]Guid idUser)
        {
            var deleteUser = await _userService.GetUserByIdAsync(idUser);
            if (deleteUser != null)
            {
                var result = await _userService.DeleteUserAsync(idUser);
                if (result)
                {
                    var response = new DeleteUserResponse
                        (new Application.Models.Dtos.UserDto(deleteUser));
                    return Ok(response);
                }
                return BadRequest("User not deleted");
            }
            return StatusCode
                (406, "No content found based on criteria");
        }
    }
}
