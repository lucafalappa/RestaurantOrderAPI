namespace RestaurantOrderAPI.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring 
    /// middlewares in an application
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// Adds necessary middlewares for an application
        /// </summary>
        /// <param name="app">The 
        /// <see cref="WebApplication"/> instance
        /// </param>
        /// <returns>The updated 
        /// <see cref="WebApplication"/> app instance
        /// </returns>
        public static WebApplication? AddApplicationMiddleware
            (this WebApplication? app)
        {
            return app;
        }
    }
}
