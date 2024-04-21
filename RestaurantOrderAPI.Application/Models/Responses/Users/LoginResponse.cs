namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    public class LoginResponse
    {
        public string Success { get; set; } 
            = string.Empty;
        public string Token { get; set; }
            = string.Empty;
        public LoginResponse(string token)
        {
            Success = "Successful log in";
            Token = token;
        }
    }
}
