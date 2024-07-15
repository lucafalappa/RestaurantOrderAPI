using FluentValidation;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Models.Requests.Restaurant.Orders;
using RestaurantOrderAPI.Application.Validators.Restaurant.Dishes;

namespace RestaurantOrderAPI.Application.Validators.Restaurant.Orders
{
    /// <summary>
    /// Validator for <see cref="CreateOrderRequest"/> objects
    /// </summary>
    public class CreateOrderRequestValidation
        : AbstractValidator<CreateOrderRequest>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CreateOrderRequestValidation"/> class.
        /// Sets up validation rules for 
        /// <see cref="CreateOrderRequest"/> fields
        /// </summary>
        public CreateOrderRequestValidation()
        {
            RuleFor(request => request.DeliveryAddress)
                .NotNull()
                .WithMessage("DeliveryAddress field is required (null)")
                .NotEmpty()
                .WithMessage("DeliveryAddress field is required (empty)")
                .IsOnlyLetters("DeliveryAddress field does not " +
                    "contain only letters");
            RuleFor(request => request.Dishes)
                .ForEach(dish =>
                    dish.SetValidator
                    (new CreateDishRequestValidation())
                );
        }
    }
}
