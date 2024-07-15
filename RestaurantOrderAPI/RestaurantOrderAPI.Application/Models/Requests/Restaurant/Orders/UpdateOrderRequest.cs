namespace RestaurantOrderAPI.Application.Models.Requests.Restaurant.Orders
{
    /// <summary>
    /// Represents a request to update an order
    /// </summary>
    public class UpdateOrderRequest
    {
        /// <summary>
        /// The new delivery address of the order
        /// </summary>
        public string DeliveryAddress { get; set; }
            = string.Empty;
    }
}
