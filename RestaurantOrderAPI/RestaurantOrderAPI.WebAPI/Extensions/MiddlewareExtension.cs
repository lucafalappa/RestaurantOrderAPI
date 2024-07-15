namespace RestaurantOrderAPI.WebAPI.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring 
    /// middlewares in a web application
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// Adds necessary middlewares for a web application
        /// </summary>
        /// <param name="app">The 
        /// <see cref="WebApplication"/> instance
        /// </param>
        /// <returns>The updated 
        /// <see cref="WebApplication"/> app instance
        /// </returns>
        public static WebApplication? AddWebAPIMiddleware
            (this WebApplication? app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
