namespace RestaurantOrderAPI.Application.Models.Requests.Orders
{
    public class GetOrdersRequest
    {
        public DateTime StartDate { get; set; }
            = DateTime.MinValue;
        public DateTime EndDate { get; set; }
            = DateTime.MaxValue;
    }
}
