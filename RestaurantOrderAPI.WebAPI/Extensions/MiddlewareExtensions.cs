namespace RestaurantOrderAPI.WebAPI.Extensions
{
    public static class MiddlewareExtensions
    {
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
