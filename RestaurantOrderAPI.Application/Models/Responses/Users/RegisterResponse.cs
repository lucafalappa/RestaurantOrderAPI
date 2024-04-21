namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    public class RegisterResponse
    {
        public string Success { get; set; } 
            = string.Empty;
        public string Token { get; set; } 
            = string.Empty;
        public RegisterResponse(string token)
        {
            Success = "Successful registration";
            Token = token;
        }
    }
}
