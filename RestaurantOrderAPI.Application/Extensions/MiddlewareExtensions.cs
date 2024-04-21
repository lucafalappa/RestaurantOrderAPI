namespace RestaurantOrderAPI.Application.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication? AddApplicationMiddleware
            (this WebApplication? app)
        {
            return app;
        }
    }
}
