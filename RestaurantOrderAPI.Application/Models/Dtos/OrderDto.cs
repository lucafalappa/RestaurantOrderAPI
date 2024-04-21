using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Dtos
{
    public class OrderDto
    {
        public Guid IdOrder { get; set; }
            = Guid.Empty;
        public DateTime OrderDate { get; set; } 
            = DateTime.Now;
        public string DeliveryAddress { get; set; }
            = string.Empty;
        public List<DishDto> OrderedDishes { get; set; }
            = null!;
        public OrderDto()
        {

        }
        public OrderDto(Order order)
        {
            IdOrder = order.IdOrder;
            OrderDate = order.OrderDate;
            DeliveryAddress = order.DeliveryAddress;
            OrderedDishes = DishDto.FromEntityToDto
                (dishes: (List<Dish>)order.OrderedDishes);
        }
        public static List<Order> FromDtoToEntity
            (List<OrderDto> dtos, Guid idOrderingUser)
        {
            var result = new List<Order>();
            foreach (var dto in dtos)
            {
                result.Add(new Order()
                {
                    IdOrder = dto.IdOrder,
                    OrderDate = dto.OrderDate,
                    DeliveryAddress = dto.DeliveryAddress,
                    OrderedDishes = DishDto.FromDtoToEntity
                        (dto.OrderedDishes, dto.IdOrder),
                    IdUser = idOrderingUser
                });
            }
            return result;
        }
        public static List<OrderDto> FromEntityToDto
            (List<Order> orders)
        {
            if (orders != null)
            {
                var result = new List<OrderDto>();
                foreach (var order in orders)
                {
                    result.Add(new OrderDto()
                    {
                        IdOrder = order.IdOrder,
                        OrderDate = order.OrderDate,
                        DeliveryAddress = order.DeliveryAddress,
                        OrderedDishes = DishDto.FromEntityToDto
                            (dishes: (List<Dish>)order.OrderedDishes)
                    });
                }
                return result;
            }
            return new List<OrderDto>();
        }
    }
}
