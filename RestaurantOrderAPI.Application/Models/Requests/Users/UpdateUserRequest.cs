﻿namespace RestaurantOrderAPI.Application.Models.Requests.Users
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
            = string.Empty;
        public string Name { get; set; }
            = string.Empty;
        public string Surname { get; set; }
            = string.Empty;
        public string Password { get; set; }
            = string.Empty;
        public string UserRole { get; set; }
            = string.Empty;
    }
}
