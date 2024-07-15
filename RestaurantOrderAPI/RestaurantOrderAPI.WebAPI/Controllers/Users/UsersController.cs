using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.Application.Abstractions.Configurations;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Abstractions.Services.Users;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Models.Responses.Users;
using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderAPI.WebAPI.Controllers.Users
{
    /// <summary>
    /// Controller responsible for user-related operations 
    /// in the web application
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// The service for managing users
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// The service for managing tokens
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// The utility class for password 
        /// encoding and verification
        /// </summary>
        private readonly IPasswordEncoder _passwordEncoder;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UsersController"/> class
        /// </summary>
        /// <param name="userService">The user service instance
        /// </param>
        /// <param name="tokenService">The token service instance
        /// </param>
        /// <param name="passwordEncoder">The password encoder instance
        /// </param>
        public UsersController
            (IUserService userService, ITokenService tokenService,
            IPasswordEncoder passwordEncoder)
        {
            _userService = userService;
            _tokenService = tokenService;
            _passwordEncoder = passwordEncoder;
        }
        /// <summary>
        /// Endpoint for user login
        /// </summary>
        /// <param name="request">The login request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// login operation result
        /// </returns>
        [HttpPost]
        [Route("/users/login")]
        public async Task<IActionResult> LoginAsync
            ([FromBody][Required] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var users = await _userService.GetUsersAsync
                    (null, user => user.Email.Equals(request.Email));
                var userDto = users.FirstOrDefault();
                if (userDto != null)
                {
                    if (_passwordEncoder.VerifyPassword
                        (request.Password, userDto.Password))
                    {
                        var response = new LoginResponse
                            (_tokenService.CreateToken(userDto));
                        return StatusCode(200, response);
                    }
                }
                return StatusCode(400, "Invalid email or password");
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for user registration
        /// </summary>
        /// <param name="request">The register request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// registration operation result
        /// </returns>
        [HttpPost]
        [Route("/users/register")]
        public async Task<IActionResult> RegisterAsync
            ([FromBody][Required] RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var idUser = Guid.NewGuid();
                var token = _tokenService.CreateToken(idUser, request);
                var userDto = request.ToDto(idUser);
                userDto.Password = _passwordEncoder.EncodePassword
                    (request.Password);
                var result = await _userService.CreateUserAsync
                        (userDto, user => user.Email.Equals(userDto.Email));
                if (result != null)
                {
                    var response = new RegisterResponse(token);
                    return StatusCode(201, response);
                }
                return StatusCode(500, "User not created");
            }
            return StatusCode(400, "Invalid request");
        }
        /// <summary>
        /// Endpoint for user authentication
        /// </summary>
        /// <param name="request">The authenticate request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// authentication operation result
        /// </returns>
        [HttpPost]
        [Route("/users/auth")]
        public async Task<IActionResult> AuthenticateAsync
            ([FromBody][Required] AuthenticateRequest request)
        {
            if (ModelState.IsValid)
            {
                if (!await _userService.NotUniqueAdministrator(null))
                {
                    var idUser = Guid.NewGuid();
                    var token = _tokenService.CreateToken(idUser, request);
                    var userDto = request.ToDto(idUser);
                    userDto.Password = _passwordEncoder.EncodePassword
                        (request.Password);
                    var result = await _userService.CreateUserAsync
                        (userDto, user => user.Email.Equals(userDto.Email));
                    if (result != null)
                    {
                        var response = new AuthenticateResponse(token);
                        return StatusCode(201, response);
                    }
                    return StatusCode(500, "User not created");
                }
                return StatusCode(400, "Administrator already existing");
            }
            return StatusCode(400, "Invalid request");
        }
        /// <summary>
        /// Endpoint for retrieving a user by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the user to retrieve
        /// </param>
        /// <returns>An IActionResult representing the 
        /// user retrieval operation result
        /// </returns>
        [HttpGet]
        [Route("/users/{id}")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme,
        Policy = "AdministratorPolicy")]
        public async Task<IActionResult> GetUserByIdAsync
            ([FromRoute][Required] Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result != null)
            {
                var response = new GetUserResponse(result);
                return StatusCode(200, response);
            }
            return StatusCode(406, "No content found based on criteria");
        }
        /// <summary>
        /// Endpoint for updating a user's information
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the user to update
        /// </param>
        /// <param name="request">The update user request object
        /// </param>
        /// <returns>An IActionResult representing the 
        /// update user operation result
        /// </returns>
        [HttpPut]
        [Route("/users/{id}")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme,
        Policy = "AdministratorPolicy")]
        public async Task<IActionResult> UpdateUserAsync
            ([FromRoute][Required] Guid id,
            [FromBody][Required] UpdateUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var userDto = await _userService.GetUserByIdAsync(id);
                if (userDto != null)
                {
                    userDto.Email = request.Email;
                    userDto.Name = request.Name;
                    userDto.Surname = request.Surname;
                    userDto.Password = _passwordEncoder.EncodePassword
                        (request.Password);
                    await _userService.UpdateUserAsync
                        (userDto, user => user.IdUser.Equals(id));
                    var response = new UpdateUserResponse(userDto);
                    return StatusCode(200, response);
                }
                return StatusCode(406, "No content found based on criteria");
            }
            return StatusCode(400, "Request not valid");
        }
        /// <summary>
        /// Endpoint for deleting a user by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the user to delete
        /// </param>
        /// <returns>An IActionResult representing the 
        /// delete user operation result
        /// </returns>
        [HttpDelete]
        [Route("/users/{id}")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme,
        Policy = "AdministratorPolicy")]
        public async Task<IActionResult> DeleteUserAsync
            ([FromRoute][Required] Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result != null)
            {
                var response = new DeleteUserResponse(result);
                return StatusCode(200, response);
            }
            return StatusCode(406, "No content found based on criteria");
        }
        /// <summary>
        /// Endpoint for retrieving a list of users 
        /// with pagination and optional filters
        /// </summary>
        /// <param name="from">The starting index of the pagination
        /// </param>
        /// <param name="num">The number of items per page
        /// </param>
        /// <param name="name">Filter by user name
        /// </param>
        /// <param name="surname">Filter by user surname
        /// </param>
        /// <returns>An IActionResult representing the 
        /// users retrieval operation result
        /// </returns>
        [HttpGet]
        [Route("/users")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme,
        Policy = "AdministratorPolicy")]
        public IActionResult GetUsers
            ([FromQuery][Required] int from,
            [FromQuery][Required] int num,
            [FromQuery] string name = "",
            [FromQuery] string surname = "")
        {
            if (!(from <= 0 || num < 0))
            {
                int queryCount = 0;
                var userDtos = _userService.GetUsersWithPagination
                    (from * num, from, (user =>
                        user.Name.ToLower().Contains(name.ToLower())
                        || user.Surname.ToLower().Contains(surname.ToLower())
                    ), out queryCount);
                if (userDtos != null)
                {
                    var response = new GetUsersResponse(userDtos);
                    var foundPages = (queryCount / (decimal)(from));
                    response.PagesNumber = (int)(Math.Ceiling(foundPages));
                    return Ok(response);
                }
                return StatusCode
                    (406, "No content found based on criteria");
            }
            return StatusCode(400, "Request not valid");
        }
    }
}
